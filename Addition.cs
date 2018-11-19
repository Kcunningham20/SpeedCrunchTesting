using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.InputDevices;

namespace UnitTestProject2
{
    [TestClass]
    public class Addition
    {
        private string appPathUnderTest = @"C:\Users\kcunningham20\Desktop\SpeedCrunch";
        private string appUnderTest = @"speedcrunch.exe";
        private string windowPrefix = "SpeedCrunch";
        private string[] menuFileExit = { "Session", "Quit" };

        class AppUnderTest
        {
            public Application app;
            public Window w;
        }

        AppUnderTest aut;
        
        [TestInitialize]
        public void Init()
        {
            aut = StartApp();
        }

        [TestCleanup]
        public void Cleanup()
        {
            TerminateApp(aut);
        }

        /*
        [TestMethod]
        public void TestQuitWithoutSaving()
        {
            AppUnderTest aut = StartApp();
            if (aut.w != null)
            {
                TerminateApp(aut);
            }
            TerminateApp(aut);
        }
        */

        //TestSuite- Addition
        [TestMethod]
        public void Test_Integer_Addition()
        {
            //aut.w.Get<UIItem>("ResultDisplay");
            InterrogateApp();
            //highlight and save to the clipboard
            aut.w.Keyboard.Enter("3+4");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            InterrogateItem(aut.w);
        }

        [TestMethod]
        public void Test_Composite_Integer_Addition()
        {
            aut.w.Keyboard.Enter("(7+9)+(8+7)");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }
        
        [TestMethod]
        public void Test_Double_Addition()
        {
            aut.w.Keyboard.Enter("7.2+7.7");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }

        [TestMethod]
        public void Test_Sin_Addition()
        {
            aut.w.Keyboard.Enter("sin(pi/2)+sin(pi)");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }

        [TestMethod]
        public void Test_Cos_Addition()
        {
            aut.w.Keyboard.Enter("cos(pi)+cos(pi/2)");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }

        [TestMethod]
        public void Test_Fraction_Addition()
        {
            aut.w.Keyboard.Enter("(7/3)+(9/3)");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }

        [TestMethod]
        public void Test_8_Decimal_Addition()
        {
            aut.w.Keyboard.Enter("7.12345678+3.23456789");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }

        [TestMethod]
        public void Test_Fraction_Integer_Addition()
        {
            aut.w.Keyboard.Enter("(7/3)+3");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }

        [TestMethod]
        public void Test_Fraction_Decimal_Addition()
        {
            aut.w.Keyboard.Enter("(7/3)+3.1415926535");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }

        [TestMethod]
        public void Test_Integer_Decimal_Addition()
        {
            aut.w.Keyboard.Enter("5+3.1415926535");
            aut.w.Keyboard.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            aut.w.Keyboard.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
        }

  
        private AppUnderTest StartApp()
        {
            AppUnderTest aut = new AppUnderTest();
            var appPath = Path.Combine(appPathUnderTest, appUnderTest);
            aut.app = Application.Launch(appPath);
            var ws = aut.app.GetWindows();
            var start = DateTime.Now;
            var timeout = new TimeSpan(0, 0, 30);
            while ((ws == null || ws.Count == 0) && DateTime.Now - start < timeout)
            {
                ws = aut.app.GetWindows();
            }
            while (aut.w == null && DateTime.Now - start < timeout)
            {
                System.Diagnostics.Debug.Write(".");
                try
                {
                    foreach (var win in ws)
                    {
                        System.Diagnostics.Debug.Write(win.Title);
                        if (win.Title.StartsWith(windowPrefix))
                            aut.w = win;
                    }
                }
                catch
                {
                    //Might end up here if the app has a splash screen, and that window goes away. Refresh the windows list
                    ws = aut.app.GetWindows();
                }
            }

            //maximize window and clicks input box
            try
            {
                if (aut.w != null)
                {
                    var max = aut.w.Get<Button>("Maximize");
                    max.Click();
                    aut.w.Mouse.Location = new System.Windows.Point(10, 1030);
                    aut.w.Click();
                }
            }
            catch
            {
                aut.w.Mouse.Location = new System.Windows.Point(10, 1030);
                aut.w.Click();
            }

            return aut;
        }

        private void TerminateApp(AppUnderTest aut)
        {
            //if (aut.w.MenuBar != null)
            //{
            //    var m = aut.w.MenuBar.MenuItem();
            //    if (m != null)
            //    {
            //        m.Click();
            //    }
            //}
            //else
            //{
                aut.w.Close();
            //}

            //DontSave(aut);
        }
        /*
        private void DontSave(AppUnderTest aut)
        {
            //Check for "Save Document?" dialog
            try
            {
                Window dialog = null;
                while (dialog == null)
                {
                    var ws = aut.app.GetWindows();
                    foreach (var w in ws)
                    {
                        if (w.Title.StartsWith("SpeedCrunch"))
                            dialog = w;
                    }
                }
                //var b = dialog.Get<Button>("Don't Save");
                //b.Click();
            }
            catch
            {
                //Window went away. Do nothing
            }
        }
        */
        private void InterrogateApp()
        {
            AppUnderTest aut = StartApp();

            if (aut.w != null)
            {
                InterrogateItem(aut.w);
                TerminateApp(aut);
            }
        }

        private void InterrogateItem(Window w)
        {
            InterrogateItem(w, w);
        }

        private void InterrogateItem(Window w, IUIItem i)
        {
            System.Diagnostics.Debug.Print(i.ToString());
            w.Mouse.Location = new System.Windows.Point(i.Bounds.Left + i.Bounds.Width / 2, i.Bounds.Top + i.Bounds.Height / 2);

            if (i is UIItemContainer)
            {
                var ic = i as UIItemContainer;
                foreach (var mb in ic.MenuBars)
                {
                    System.Diagnostics.Debug.Print(mb.ToString());

                    w.Mouse.Location = new System.Windows.Point(mb.Bounds.Left + mb.Bounds.Width / 2, mb.Bounds.Top + mb.Bounds.Height / 2);

                }

                foreach (var x in ic.Items)
                {
                    InterrogateItem(w, x);

                    w.Mouse.Location = new System.Windows.Point(x.Bounds.Left + x.Bounds.Width / 2, x.Bounds.Top + x.Bounds.Height / 2);
                }
            }
        }
    }
}
