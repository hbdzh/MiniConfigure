using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniConfigure
{
    public class MiniProperty
    {
        /// <summary>
        /// 获取指定节点下的所有属性
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <returns>指定节点下的所有属性</returns>
        public static List<string> GetAllProperty(string filePath, string node)
        {
            List<string> nodeList = new List<string>();
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string subStr = "]";
                        string line;
                        while ((line = sr.ReadLine()) != null)//一行一行读取
                        {
                            if (line.Contains("[" + node + subStr))
                            {
                                string str = line.Substring(line.IndexOf(subStr) + 1, line.IndexOf(":") - line.IndexOf(subStr) - 1);
                                nodeList.Add(str);
                            }
                        }
                    }
                }
            }
            return nodeList;
        }

        /// <summary>
        /// 获取文件内的所有属性
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>指定文件内的所有属性</returns>
        public static List<string> GetAllProperty(string filePath)
        {
            List<string> nodeList = new List<string>();
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string subStr = "]";
                        string line;
                        while ((line = sr.ReadLine()) != null)//一行一行读取
                        {
                            string str = line.Substring(line.IndexOf(subStr) + 1, line.IndexOf(":") - line.IndexOf(subStr) - 1);
                            nodeList.Add(str);
                        }
                    }
                }
            }
            return nodeList;
        }

        /// <summary>
        /// 通过节点和值修改属性
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <param name="value">值</param>
        /// <param name="oldProperty">需要修改的属性</param>
        /// <param name="newProperty">修改后的属性</param>
        public static void SetProperty(string filePath, string node, string value, string oldProperty, string newProperty)
        {
            StringBuilder oldConfig = new StringBuilder();
            oldConfig.Append('[');
            oldConfig.Append(node);
            oldConfig.Append(']');
            oldConfig.Append(oldProperty);
            oldConfig.Append(':');
            oldConfig.Append(value);
            oldConfig.Append(';');

            StringBuilder newConfig = new StringBuilder();
            newConfig.Append('[');
            newConfig.Append(node);
            newConfig.Append(']');
            newConfig.Append(newProperty);
            newConfig.Append(':');
            newConfig.Append(value);
            newConfig.Append(';');
            string configInfo;
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                configInfo = sr.ReadToEnd().Replace(oldConfig.ToString(), newConfig.ToString());
            }
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(configInfo);
            }
            MiniConfig.CheckFileFormat(filePath);
        }
    }
}
