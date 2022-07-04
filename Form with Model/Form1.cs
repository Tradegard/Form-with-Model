using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using IronPython.Hosting;
using System.IO;
//using IronPython.Modules;
using Python.Runtime;

namespace Form_with_Model
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /*
        public int ironpython()
        {

            var engine = Python.CreateEngine();

            ICollection<string> searchPaths = engine.GetSearchPaths();
            searchPaths.Add(@".\Lib");
            engine.SetSearchPaths(searchPaths);

            var scope = engine.CreateScope();
            var source = engine.CreateScriptSourceFromFile(@".\pythonforc.py");
            var compiled = source.Compile();
            compiled.Execute(scope);

            object myclass = engine.Operations.Invoke(scope.GetVariable("MyClass"));
            int a = Convert.ToInt32(textBox1.Text);
            int b = Convert.ToInt32(textBox2.Text);
            int c = Convert.ToInt32(textBox2.Text);
            object[] parameters = new object[] {a,b,c};
            var result = engine.Operations.InvokeMember(myclass, "model", parameters);
            return result;

        }
        */
        public double pythonnet()

        {
            Environment.SetEnvironmentVariable("PATH", @"C:\Files\Anaconda\envs\env38", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", @"C:\Files\Anaconda\envs\env38", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", @"C:\Files\Anaconda\envs\env38\python38.dll");
            PythonEngine.Initialize();
            int x1 = Convert.ToInt32(textBox1.Text);
            int x2 = Convert.ToInt32(textBox2.Text);
            int x3 = Convert.ToInt32(textBox3.Text);

            using (Py.GIL())
            {
                dynamic j = Py.Import("joblib");
                dynamic pd = Py.Import("pandas");
                dynamic loaded_model = j.load(@"C:/Users/kuptsov_ae/source/repos/Form with Model/Form with Model/bin/Debug/finalized_model.sav");

                int[,] items = new int[1,3]
                {
                    {x1,x2,x3}
                };

                object result = loaded_model.predict(items);
                var res = result.ToString();
                res = res.Replace(@"[", "").Replace(@"]", "").Replace(@".", ",");
                double res1 = Convert.ToDouble(res);
                return res1;

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //label1.Text = ironpython().ToString();
            label1.Text = pythonnet().ToString();
        }

    }
}
