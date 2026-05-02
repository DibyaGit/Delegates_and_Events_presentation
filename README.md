# Project Artemis 🚀 | C# Delegates & Events

**Project Artemis** is a mini-simulation of a space launch system designed to demonstrate the power of **Event-Driven Architecture** in C#. 

## 📖 Overview

In complex software—like game engines, UI frameworks, or massive enterprise applications—modules need to communicate without being tightly coupled or permanently glued together. This project illustrates how to achieve a perfectly **Decoupled System**. 

Instead of the Command Center (Houston) directly calling the rocket engine's internal methods, it broadcasts an event over a secure "loudspeaker." The independent subsystems (the Listeners) simply hear the broadcast and react entirely on their own.

## 🧠 Core Concepts Demonstrated

* **Delegates (The Blueprint):** The strict "contract" defining the exact rules for what goes into and what comes out of a method.
* **Events (The Loudspeaker):** A secure, protective wrapper around a multicast delegate. It restricts outside classes so they can only subscribe (`+=`) or unsubscribe (`-=`). This prevents any outside code from accidentally deleting the entire list of listeners.
* **Standard Practice:** Skipping custom, manual delegate rulebooks in favor of the modern, built-in `EventHandler<T>`.
* **Decoupled Architecture:** Systems talking to each other independently without direct or hardcoded dependencies.

## 🏗️ System Architecture

The program is split into three distinct roles based on the standard Event pattern:

1. **The Payload (`SystemStatusEventArgs`):** 
   A custom class inheriting from `EventArgs` that securely packages our broadcast data. It carries a `PowerLevel` (integer) and a `DiagnosticMessage` (string).

2. **The Broadcaster (`CentralCommand`):** 
   This is our "Houston." It holds the loudspeaker. It defines the `SystemStatusUpdated` event and safely triggers it using the `?.Invoke()` operator, which ensures the system only broadcasts if at least one listener is actively subscribed.

3. **The Listeners:** 
   * `PropulsionSystem`: Subscribes to the broadcast and independently evaluates if the power level is sufficient for a safe launch (>= 85%).
   * `TelemetryService`: Subscribes to log incoming diagnostic messages and system statuses.

## 💻 Code Execution

In `Program.cs`, the architecture is wired together. We instantiate our independent objects and subscribe the listeners to the command center using the `+=` operator. 

We trigger an initial broadcast to show both listeners reacting. Then, we demonstrate how to safely detach a listener using the `-=` operator (simulating the Telemetry going offline) before sending a final launch broadcast.

## 🖥️ Console Output

When you run the application, you will see the following output in the console:
```text
--- System Initialization ---

--- Initial Broadcast ---

[Central Command]: Broadcasting system update...
[Propulsion System]: Acknowledged. Current power level is at 60%.
 -> Status: Warning. Insufficient power.
[Telemetry Service]: Logging diagnostic update - Message: 'Pre-flight checks ongoing.'

[System Alert]: Telemetry Service is going offline for maintenance.

--- Secondary Broadcast ---

[Central Command]: Broadcasting system update...
[Propulsion System]: Acknowledged. Current power level is at 95%.
 -> Status: Optimal. Ready for execution.

Press Enter to exit...
