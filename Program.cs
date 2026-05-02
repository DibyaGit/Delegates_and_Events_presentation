using System;

namespace EventDrivenArchitectureDemo
{
    // STEP 1: DEFINE THE EVENT DATA (The Payload)
    // This class holds the data that will be broadcasted to all subscribers.
    // It must inherit from EventArgs.
    public class SystemStatusEventArgs : EventArgs
    {
        public int PowerLevel { get; set; }
        public string? DiagnosticMessage { get; set; }
    }

    // STEP 2: CREATE THE PUBLISHER (The Broadcaster)
    // This class defines the event and contains the logic to trigger it.
    public class CentralCommand
    {
        // Define the event using the built-in EventHandler<T> delegate.
        // The '?' indicates that the event can be null (if there are zero subscribers).
        public event EventHandler<SystemStatusEventArgs>? SystemStatusUpdated;

        // Method to initiate the event broadcast
        public void BroadcastStatus(int powerLevel, string message)
        {
            Console.WriteLine("\n[Central Command]: Broadcasting system update...");

            // Package the data into our EventArgs class
            SystemStatusEventArgs currentStatus = new SystemStatusEventArgs
            {
                PowerLevel = powerLevel,
                DiagnosticMessage = message
            };

            // Safely invoke the event. 
            // The '?.' operator ensures we only broadcast if at least one subscriber is listening.
            SystemStatusUpdated?.Invoke(this, currentStatus);
        }
    }

    // STEP 3: CREATE THE SUBSCRIBERS (The Listeners)
    // These classes contain methods that match the EventHandler signature and react to the broadcast.

    public class PropulsionSystem
    {
        // The signature must match: (object? sender, SystemStatusEventArgs e)
        public void HandleSystemUpdate(object? sender, SystemStatusEventArgs e)
        {
            Console.WriteLine($"[Propulsion System]: Acknowledged. Current power level is at {e.PowerLevel}%.");

            if (e.PowerLevel >= 85)
            {
                Console.WriteLine(" -> Status: Optimal. Ready for execution.");
            }
            else
            {
                Console.WriteLine(" -> Status: Warning. Insufficient power.");
            }
        }
    }

    public class TelemetryService
    {
        public void LogDiagnosticData(object? sender, SystemStatusEventArgs e)
        {
            Console.WriteLine($"[Telemetry Service]: Logging diagnostic update - Message: '{e.DiagnosticMessage}'");
        }
    }

    // STEP 4: WIRE THE ARCHITECTURE TOGETHER (Execution)
    // The entry point of the application where objects are created and events are subscribed to.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- System Initialization ---");

            // 4a. Instantiate the publisher and subscribers
            CentralCommand commandCenter = new CentralCommand();
            PropulsionSystem mainEngine = new PropulsionSystem();
            TelemetryService telemetry = new TelemetryService();

            // 4b. Subscribe to the event
            // Attach the subscriber methods to the publisher's event
            commandCenter.SystemStatusUpdated += mainEngine.HandleSystemUpdate;
            commandCenter.SystemStatusUpdated += telemetry.LogDiagnosticData;

            // 4c. Trigger the event
            // Both the engine and telemetry service will react to this broadcast
            Console.WriteLine("\n--- Initial Broadcast ---");
            commandCenter.BroadcastStatus(60, "Pre-flight checks ongoing.");

            // 4d. Unsubscribe from the event
            // Detach a subscriber when it no longer needs to listen
            Console.WriteLine("\n[System Alert]: Telemetry Service is going offline for maintenance.");
            commandCenter.SystemStatusUpdated -= telemetry.LogDiagnosticData;

            // 4e. Trigger the event again
            // Only the engine will react this time, as telemetry has unsubscribed
            Console.WriteLine("\n--- Secondary Broadcast ---");
            commandCenter.BroadcastStatus(95, "All systems go.");

            // Keep the console window open
            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}


