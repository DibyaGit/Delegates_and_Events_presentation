# Project Artemis 🚀 | C# Delegates & Events

**Project Artemis** is a mini-simulation of a space launch system designed to demonstrate the power of **Event-Driven Architecture** in C#. 

## 📖 Overview
In complex software (like game engines or enterprise applications), modules need to communicate without being tightly coupled—or permanently glued together[cite: 2]. This project illustrates how to achieve a perfectly **Decoupled System**[cite: 2]. 

Instead of the Command Center (Houston) directly calling the rocket engine's methods, it broadcasts an event over a secure "loudspeaker." The independent subsystems (Listeners) hear the broadcast and react on their own[cite: 2]. 

## 🧠 Core Concepts Demonstrated
* **Delegates (The Blueprint):** The strict "contract" defining the rules for what goes into and comes out of a method[cite: 2].
* **Events (The Loudspeaker):** A secure, protective wrapper around a multicast delegate. It restricts outside classes so they can only subscribe (`+=`) or unsubscribe (`-=`), preventing accidental deletion of the listener list[cite: 2].
* **Standard Practice:** Skipping custom delegate rulebooks in favor of the modern, built-in `EventHandler<T>`[cite: 2].
* **Decoupled Architecture:** Systems talking to each other independently without direct dependencies[cite: 2].

## 🏗️ System Architecture
The program is split into three specific roles based on the Event pattern[cite: 2]:

1. **The Payload (`SystemStatusEventArgs`):** A custom class inheriting from `EventArgs` that packages our broadcast data: `PowerLevel` and `DiagnosticMessage`[cite: 1].
2. **The Broadcaster (`CentralCommand`):** Holds the loudspeaker. It defines the `SystemStatusUpdated` event and safely triggers it using the `?.Invoke()` operator to ensure someone is listening before broadcasting[cite: 1].
3. **The Listeners:** 
   * `PropulsionSystem`: Subscribes to the broadcast and evaluates if the power level is sufficient for launch (>= 85%)[cite: 1].
   * `TelemetryService`: Subscribes to log incoming diagnostic messages[cite: 1].

## 🚀 Execution & Sample Output
In `Program.cs`, the architecture is wired together. We instantiate our objects, subscribe the listeners to the command center using `+=`, trigger a broadcast, and then demonstrate how to safely detach a listener using `-=`[cite: 1].

**Console Output:**
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
