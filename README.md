# health-monitor

## Health monitor server description

Health Monitor measures values and send notifications to all connected clients.

Server can be reached via WebSocket connection. Current location is *ws://127.0.0.1:8181*

---
### Server notification message structure

Server provides notifications in json format

#### General structure
Field | Type | Description
--- | --- | ---
messageType | `string` | *type of notification message*
message | `string` | *message description*
data | `object` | *measured data*

#### Notification messages list:
 1. ##### Information about current running processes 
 
    Message is sended every 2 seconds

    __messageType__ : ProcessesInformation

    __message__ : Running processes information
    
    __data__ : `object`
    
    Field | Type | Description
    --- | --- | ---
    id | `int` | Process id
    name | `string` | Process name
    memoryUsage | `long` | Amount of physical memory, in bytes, allocated for the associated process
    totalProcessorTime | `int` | Total processor time for this process in milliseconds
 2. ##### CPU highload warning 
 
    Message is sended when Processor Time used exceeds 80%

    __messageType__ : CpuHighload

    __message__ : Warning highload
    
    __data__ : `float` Current CPU load in %
 3. ##### Memory highload warning
 
    Message is sended when Available memory is less than 10%

    __messageType__ : LowMemory

    __message__ : Warning low memory
    
    __data__ : `float` Available memory in Mb
