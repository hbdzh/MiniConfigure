# 迷你配置文件

## 简介

迷你配置文件可以用来存储任何简单信息，不应该只把他当作程序的配置文件。

一个标准的迷你配置文件，应该是下面这样：

```
[Node]Property:Value;
```

**我自己制定的一些规范如下：**

1. 括号中的我把他叫作“节点”，节点后面跟着的是“属性”，最后的是“值”。
2. 每一条信息最后都应该以英文状态下的顿号结尾。
3. 每一行都只应该有一条信息。

## 使用方法

### 创建一个迷你配置文件

引入命名空间

```c#
using MiniConfigure;
```

创建迷你配置文件的方法在MiniConfig类中。

第一个参数：文件路径

第二个参数：节点名

第三个参数：属性名

第四个参数：值

每次新建迷你配置文件时都应该设置一条信息。

```c#
MiniConfig.CreateFile(@"C:\App.mini","Font","FontSize","16");
```

以上，存放了一条信息的迷你配置文件就创建完成了。

### 添加一条信息

下面的代码，将帮助我们向App.mini这个文件中添加一条新的信息。

```c#
MiniConfig.AppendConfig(@"C:\App.mini","Font","FontColor","Black");
```

如果你担心自己的迷你配置文件可能不规范。

这里也提供了一个方法，将文件规范化。

但是我不推荐你使用，因为在其他方法中，已经调用了这个方法。

下面的代码是用来将迷你配置文件规范化。

```c#
MiniConfig.CheckFileFormat(@"C:\App.mini");
```

### 获取信息中的值

获取值的相关方法在MiniValue类中。

第一个参数：文件路径

第二个参数：节点名

第三个参数：属性名

使用方法很简单，请看下面的代码：

```c#
MiniValue.GetValue(@"C:\App.mini","Font","FontSize");
```

返回值是string类型，返回的值是16。

### 修改某个值

前几个参数和GetValue是一样的，后面两个参数分别是旧的值和新的值。

具体请看下面代码：

```c#
MiniValue.SetValue(@"C:\App.mini","Font","FontColor","Black","White");
```

## 最后

先说一下我的代码水平不高，也不是专业程序员，第一次把自己写的代码放上来，还有点不好意思。

为了证明这玩意儿是可用的，我已经自己编写了几个程序，其中的很多信息就是用这个来存放的。

我用他存放过程序的主题信息、设置信息，也用他实现过多语言。

为什么要自己实现，为什么不用json或者xml？

首先xml太庞大了，我的需求很简单，用不着他。

而json读起来太反人类了，我也不是很擅长，而且相对于我而言，还是有点儿复杂。

所以干脆就自己撸袖子上了。
