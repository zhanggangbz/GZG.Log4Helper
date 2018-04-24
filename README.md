# GZG.Log4Helper
封装好的可简单使用的log4net

默认生成日志在程序目录下的logs文件夹中

日志每天生成一个文件

使用方法

引入dll

//写日志记录
LogHelper.Instance.WriteInf("日志内容");
