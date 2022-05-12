# Originlab Robot 项目

![结构](./pic/func.png)

此项目是为了更有效的导入实验数据所写，在使用上需要有一下注意几点

1. 使用如果出现问题，那么最好和本地机器中的 origin.dll 重新编译一次才能使用，这可能是官方为了稳定起见做的考虑。

2. 本工具通过命令工作，一般情况下指定数据文本文件所在的父级文件夹即可，像下面这样：

   `originbot -d "D:\Users\codelib\OriginBot\datagroup"`

   其中，路径 `datagroup` 下应当有多个文件夹，一个文件夹代表一个工作簿，其下面的一个文本文件代表一张工作表

3. 程序对于实验数据的列名以及单位于设置文件中的内容且有一定的格式要求，具体格式参考例子如下

   ```txt
   X-弧长-mm
   Y-电子密度-1/m^3
   ```

4. 原则上讲，一个工作簿应当配置一个 `settings.txt`,如果缺少可能会出现问题
   
   



