# FreeSrun #
FreeSrun is an open Srun3000 client which is widely used in university campus in China developed in C#. However, the official version forbids wifi-share, VM-bridged-networking. To break these limits, FreeSrun is created. There is no any limits or inspection on your software / hardware environment, it only provides authentication function.

FreeSrun can start with command arguments. Here are all the available arguments:

|	Arguments       |              Description        |
|:------------------|:--------------------------------|
|   -u              |              Username           |
|   -p              |              Password           |
|   -add            | Authentication server IP address|
|   [-lp]           |       Login/ Logout port        |
|   [-hp]           |     Heartbeat packets port      |
|   [-hi]           |     Heartbeat packets interval, in minute, decimal support, default as 2  |
|   [-nl]           |Notify balloon duration, in second, default as 3|
|   [-to]           |       Timestamp offset          |
|   [-l]            |             Log level           |
|   [-?/ -h/ -help] |       Show arguments list       |

The available options for argument -l stand for log level are as follows:

|Available option|Description|
|:---------------|:----------|
|    debug       |Debug mode, has the most detailed log.|
|    d           |Shortform for debug|
|    warning     |Warning mode, onlyn log for exceptions and errors|
|    w           |Shortform for warning|
|    none        |None mode, log nothing|
|    no          |Shortform for none|
|    n           |Shortform for none|

**※Arguments wrapped with percent sign(%) are optional. You can start it like:**

    FreeSrun.exe -u %USERNAME% -p %PASSWORD% -add %IPADD% -nl 3 -hi 1.5 -l d

FreeSrun 1.0.4 is now available.

**Reference:**

[1] [srun3000协议简析及php登录示例](http://blog.5istar.net/?p=357 "srun3000协议简析及php登录示例")

[2] [Srun3000 客户端协议分析](http://sskaje.blogspot.com/2009/04/srun3000.html "Srun3000 客户端协议分析") (GFW Certificated)

 
**Acknowledgement:**

This project won't be created without BITers' works. Thanks for their code that inspired me alot.
