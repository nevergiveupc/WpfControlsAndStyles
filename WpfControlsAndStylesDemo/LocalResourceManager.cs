using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Diagnostics;

namespace WpfControlsAndStylesDemo
{
     /// <summary>
    /// 本地化资源管理类
    /// </summary>
    public class LocalResourceManager
    {
        /// <summary>
        /// 类的唯一实例
        /// </summary>
        private static LocalResourceManager resourceManagerInstance = null;

        private string currentCulture;
        private bool isSuccess;
        private ResourceManager resourceManagerObj;
        private string resourcePath;
        private string strModuleName;
        private ResourceDictionary languageResource;

        /// <summary>
        /// 当前区域文化
        /// </summary>
        public string CurrentCulture
        {
            get { return currentCulture; }
            set { currentCulture = value; }
        }

        /// <summary>
        /// 是否初始化创建资源对象和语言区域成功
        /// </summary>
        public bool IsSuccess
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        }

        /// <summary>
        /// 资源管理对象
        /// </summary>
        public ResourceManager ResourceManagerObj
        {
            get { return resourceManagerObj; }
            set { resourceManagerObj = value; }
        }

        /// <summary>
        /// 语言资源路径
        /// </summary>
        public string ResourcePath
        {
            get { return resourcePath; }
            set { resourcePath = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private LocalResourceManager()
        {
            this.resourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Language");
            if (!Directory.Exists(this.resourcePath))
                Directory.CreateDirectory(this.resourcePath);
            this.currentCulture = "zh-CN";
            this.strModuleName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.ModuleName);
            this.strModuleName = Path.GetFileNameWithoutExtension(this.strModuleName);
            this.isSuccess = false;
            this.resourceManagerObj = null;
            //
            bool result = InitialResourceManagerObj();

            if (!result)
            { 
                languageResource = new ResourceDictionary();
                bool createResult =SetLanguageSource(new Uri(string.Format("Language/{0}.{1}.xaml", this.strModuleName, CurrentCulture), UriKind.RelativeOrAbsolute));
                if (!createResult)
                { 
                    this.currentCulture = "en-US";
                    createResult = SetLanguageSource(new Uri(string.Format("Language/{0}.{1}.xaml", this.strModuleName, CurrentCulture), UriKind.RelativeOrAbsolute));
                }
            }
            this.isSuccess = true;
        }

        /// <summary>
        /// 初始化创建资源关联对像实例
        /// </summary>
        /// <returns></returns>
        private bool InitialResourceManagerObj()
        {
            //搜索查询指定路径下的.xaml文件，获取目标文件
            string fileNameExpress = string.Format("{0}.*.xaml", this.strModuleName);            //PecsChart.*.xaml
            string[] targetFileNames = Directory.GetFiles(this.resourcePath, fileNameExpress, SearchOption.TopDirectoryOnly);

            //解析目标文件的区域设置信息 PecsChart.zh-CN.xaml
            //只运行存在一个资源文件
            string resourceFileName = string.Empty;
            if (targetFileNames.Length == 1)
            {
                resourceFileName = Path.GetFileName(targetFileNames[0]);        //PecsChart.en-US.xaml
                string[] brokenStr = resourceFileName.Split(new char[] { '.' });
                if (brokenStr.Length == 3)
                    this.currentCulture = brokenStr[1].Trim();
                else
                    return false;
            }
            else
                return false;

            //将区域文化设置到当前进程中
            SetCulture(currentCulture);
            //创建资源管理对象
            string langFilePath =Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Language",resourceFileName);
            FileStream fileStream = new FileStream(langFilePath, FileMode.Open);
            languageResource = XamlReader.Load(fileStream) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(languageResource);
            fileStream.Close();

            return true;
        }

        /// <summary>
        /// 获取资源管理唯一实例对像
        /// </summary>
        /// <returns></returns>
        public static LocalResourceManager GetInstance()
        {
            if (resourceManagerInstance == null)
            {
                resourceManagerInstance = new LocalResourceManager();
            }

            return resourceManagerInstance;
        }

        public static void Dispose()
        {
            if (resourceManagerInstance != null)
                resourceManagerInstance = null;
        }
        /// <summary>
        /// 根据键值获取字符串
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="defaultStr">默认字符串</param>
        /// <returns></returns>
        public string GetString(string key, string defaultStr)
        {
            if (resourceManagerInstance != null)
            {
                if (this.isSuccess)
                {
                    try
                    {
                        return (string)languageResource[key];
                    }
                    catch (Exception ex)
                    {
                        return defaultStr;
                    }
                }
                else
                    return defaultStr;
            }

            else
                return defaultStr;
        }

        /// <summary>
        /// 设置当前区域文化
        /// </summary>
        /// <param name="name"></param>
        private void SetCulture(string name)
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(name);
        }

        private bool SetLanguageSource(Uri uriSource)
        {
            try
            {
                languageResource.Source = uriSource;
                Application.Current.Resources.MergedDictionaries.Add(languageResource);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
