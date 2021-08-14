using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniConfigure
{
    public class MiniValue
    {
        /// <summary>
        /// 通过节点和属性获取值列表
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <param name="property">属性名</param>
        /// <returns>获取到的值列表</returns>
        public static List<string> GetValueList(string filePath, string node, string property)
        {
            try
            {
                List<string> valueList = new List<string>();
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        StringBuilder configHeader = new StringBuilder();//存储配置信息的前半部分
                        configHeader.Append('[');
                        configHeader.Append(node);
                        configHeader.Append(']');
                        configHeader.Append(property);
                        configHeader.Append(':');

                        string configInfo;
                        while ((configInfo = sr.ReadLine()) != null)//一行一行读取
                        {
                            if (configInfo.Contains(configHeader.ToString()))//直接对该行进行判断
                            {
                                valueList.Add(configInfo.Replace(configHeader.ToString(), null).Replace(";", null));
                            }
                        }
                    }
                }
                return valueList;
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 通过节点和属性获取值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <param name="property">属性名</param>
        /// <returns>获取到的值</returns>
        public static string GetValue(string filePath, string node, string property)
        {
            if (GetValueList(filePath, node, property).Count == 1)//如果等于1说明有且只有一个值
            {
                return GetValueList(filePath, node, property)[0];//把这个值返回
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取文件内的所有值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>指定文件内的所有值</returns>
        public static List<string> GetAllValue(string filePath)
        {
            List<string> nodeList = new List<string>();
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string subStr = ":";
                        string line;
                        while ((line = sr.ReadLine()) != null)//一行一行读取
                        {
                            string str = line.Substring(line.IndexOf(subStr) + 1, line.IndexOf(";") - line.IndexOf(subStr) - 1);
                            nodeList.Add(str);
                        }
                    }
                }
            }
            return nodeList;
        }

        /// <summary>
        /// 获取指定节点下的所有值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <returns>指定节点下的所有值</returns>
        public static List<string> GetAllValue(string filePath, string node)
        {
            List<string> nodeList = new List<string>();
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string subStr = ":";
                        string line;
                        while ((line = sr.ReadLine()) != null)//一行一行读取
                        {
                            if (line.Contains("[" + node + "]"))
                            {
                                string str = line.Substring(line.IndexOf(subStr) + 1, line.IndexOf(";") - line.IndexOf(subStr) - 1);
                                nodeList.Add(str);
                            }
                        }
                    }
                }
            }
            return nodeList;
        }

        /// <summary>
        /// 通过节点和属性修改值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <param name="property">属性</param>
        /// <param name="oldValue">需要修改的值</param>
        /// <param name="newValue">要修改为的值</param>
        public static void SetValue(string filePath, string node, string property, string oldValue, string newValue)
        {
            if (File.Exists(filePath))
            {
                StringBuilder oldConfig = new StringBuilder();
                oldConfig.Append('[');
                oldConfig.Append(node);
                oldConfig.Append(']');
                oldConfig.Append(property);
                oldConfig.Append(':');
                oldConfig.Append(oldValue);
                oldConfig.Append(';');

                StringBuilder newConfig = new StringBuilder();
                newConfig.Append('[');
                newConfig.Append(node);
                newConfig.Append(']');
                newConfig.Append(property);
                newConfig.Append(':');
                newConfig.Append(newValue);
                newConfig.Append(';');
                string configInfo;
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    configInfo = sr.ReadToEnd().Replace(oldConfig.ToString(), newConfig.ToString());//替换旧的值
                }
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    sw.Write(configInfo);//将新的配置信息写入文件
                }
                MiniConfig.CheckFileFormat(filePath);
            }
        }
    }
}
