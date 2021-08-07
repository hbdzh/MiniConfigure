using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniConfigure
{
    public class MiniConfig
    {
        /// <summary>
        /// 创建一个迷你配置文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        public static void CreateFile(string filePath, string node, string property, string value)
        {
            StringBuilder config = new StringBuilder();//存储配置信息
            config.Append('[');
            config.Append(node);
            config.Append(']');
            config.Append(property);
            config.Append(':');
            config.Append(value);
            config.Append(';');
            config.Append(Environment.NewLine);
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(config.ToString());//将配置信息写进文件
                }
            }
        }
        /// <summary>
        /// 添加一行配置信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        public static void AppendConfig(string filePath, string node, string property, string value)
        {
            StringBuilder config = new StringBuilder();//存储配置信息
            config.Append('[');
            config.Append(node);
            config.Append(']');
            config.Append(property);
            config.Append(':');
            config.Append(value);
            config.Append(';');
            config.Append(Environment.NewLine);
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(config.ToString());//将配置信息添加到文件
                }
            }
        }
        /// <summary>
        /// 检查文件格式是否规范
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void CheckFileFormat(string filePath)
        {
            List<string> configList = new List<string>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        configList.Add(line);
                    }
                }
            }
            for (int i = 0; i < configList.Count; i++)
            {
                if (configList[i].Contains(";["))//将同一行拆为两行
                {
                    configList[i] = configList[i].Replace(";[", ";" + Environment.NewLine + "[");
                }
                configList.Remove("");//移除空行
            }
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var item in configList)
                {
                    sw.WriteLine(item);//将配置信息添加到文件
                }
            }
        }
    }
}
