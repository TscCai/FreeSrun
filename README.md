FreeSrun is an open Srun3000 client which is widely used in university campus in China developed in C#. However, the official version forbids wifi-share, VM-bridged-networking. To break these limits, FreeSrun is created. There is no any limits or inspection on your software / hardware environment, it only provides authentication function.

FreeSrun can start with command arguments. Here are all the available arguments:

-u	Username
-p	Password
-add	Authentication server IP address
[-lp]	Login/ Logout port
[-hp]	Heartbeat packets port
[-nl]	Notify duration, in second
[-to]	Timestamp offset
[-?/ -h/ -help]	Show arguments list
※Arguments wrapped with brackets are optional. You can start it like: 

FreeSrun.exe -u %USERNAME% -p %PASSWORD% -add %IPADD% -nl 3

 

FreeSrun 1.0.2 stable is now available.

Reference:

[1] srun3000协议简析及php登录示例

[2] Srun3000 客户端协议分析 (GFW Certificated)

 

Acknowledgement:

The advent of this project is inseparable from BITers' help. Thanks for their works!
