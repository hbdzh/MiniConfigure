using System.IO;
using System.Text;

namespace MiniConfigure
{
    class MiniProperty
    {
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
