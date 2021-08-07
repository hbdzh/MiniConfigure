using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniConfigure
{
    public class MiniNode
    {
        /// <summary>
        /// 通过节点获取该节点下的配置信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <returns>指定节点下的配置信息</returns>
        public static List<string> GetNodeInfo(string filePath, string node)
        {
            List<string> nodeList = new List<string>();
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)//一行一行读取
                        {
                            if (line.Contains(node))//直接对该行进行判断
                            {
                                nodeList.Add(line);
                            }
                        }
                    }
                }
            }
            return nodeList;
        }

        /// <summary>
        /// 通过节点获取该节点下的配置信息（不包含节点本身）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="node">节点</param>
        /// <returns>指定节点下的配置信息（不包含节点本身）</returns>
        public static List<string> GetNoNodeInfo(string filePath, string node)
        {
            List<string> nodeList = GetNodeInfo(filePath, node);
            if (nodeList.Count > 0)
            {
                return new List<string>();
            }
            List<string> propertyList = new List<string>();
            //必须使用倒序for来删除nodeList的属性
            for (int i = nodeList.Count - 1; i >= 0; i--)
            {
                //如果nodeList的某一个属性里包含了[则继续进行
                int i1 = nodeList[i].IndexOf('[');
                if (i1 == -1)
                {
                    break;
                }
                int i2 = nodeList[i].IndexOf(']');
                if (i2 == -1)
                {
                    break;
                }
                //将处理后的不带节点的属性放入新的list中
                propertyList.Add(nodeList[i].Remove(i1, i2 - i1 + 1));
                //移除已经加入新list的属性，直到为空
                nodeList.Remove(nodeList[i]);
            }
            return propertyList;
        }

        /// <summary>
        /// 通过属性和值修改节点
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <param name="oldNode">需要修改的节点</param>
        /// <param name="newNode">修改后的节点</param>
        public static void SetNode(string filePath, string property, string value, string oldNode, string newNode)
        {
            StringBuilder oldConfig = new StringBuilder();
            oldConfig.Append('[');
            oldConfig.Append(oldNode);
            oldConfig.Append(']');
            oldConfig.Append(property);
            oldConfig.Append(':');
            oldConfig.Append(value);
            oldConfig.Append(';');

            StringBuilder newConfig = new StringBuilder();
            newConfig.Append('[');
            newConfig.Append(newNode);
            newConfig.Append(']');
            newConfig.Append(property);
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
