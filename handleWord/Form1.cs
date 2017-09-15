using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms; 
namespace handleWord
{
   

    public partial class Form1 : Form
    {
 

         

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("User32.dll ")]  //只能找到父窗体的直接子窗体
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);

        //[DllImport("user32.dll", EntryPoint = "SendMessageA")]
        //public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, Point lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")] 
       public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

       
         

        [DllImport("user32.dll", EntryPoint = "SetWindowText", CharSet = CharSet.Ansi)]
        public static extern int SetWindowText(int hwnd, string lpString);

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]//调用system目录下的user32.dll动态链接库，并声明应用的过程名称
        public static extern int WindowFromPoint( int xPoint,int yPoint);

        //获得句柄的内容
        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);


        [DllImport("user32.dll", EntryPoint = "SendMessageA")] 
        private static extern int SendMessage_Ex(IntPtr hwnd, int wMsg, int wParam, StringBuilder lParam);

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

       
        [DllImport("gdi32.dll")] 
        public static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("gdi32.dll")] 
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")] 
        public static extern bool ExtFloodFill(IntPtr hdc, int nXStart, int nYStart,int crColor, uint fuFillType);
         
  
        [DllImport("gdi32.dll")]
        public static extern int GetPixel(IntPtr hdc, int x, int y);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hdc);
         
         

        [DllImport("gdi32.dll ")]
        public static extern int CreatePen(int nPenStyle, int nWidth, int crColor); 

        [DllImport("gdi32.dll")] 
        private static extern IntPtr LineTo(IntPtr hdc, IntPtr x, IntPtr y);

        public const int WM_SETTEXT = 0x000C;

        public const int WM_CHAR = 0x0102;

        public const int EM_GETLINE = 0xc4;

        public const int WM_GETTEXT = 0x000D;

        public const int WM_IME_KEYDOWN = 0x0290;

        public const int WM_IME_KEYUP = 0x0291;

        public const int WM_LBUTTONDOWN = 0x0201;

        public const int WM_LBUTTONUP = 0x0202;

        public const int WM_MOUSEMOVE = 0x0200;

        public const int MK_LBUTTON = 0x0001;

        public const int WM_PRINTCLIENT = 0x0318;

        public const int WM_PRINT = 791;

        //模拟鼠标，所以只需定义鼠标的flag值：
        const int MouseEvent_Absolute = 0x8000;
        const int MouserEvent_Hwheel = 0x01000;
        const int MouseEvent_Move = 0x0001;
        const int MouseEvent_Move_noCoalesce = 0x2000;
        const int MouseEvent_LeftDown = 0x0002;
        const int MouseEvent_LeftUp = 0x0004;
        const int MouseEvent_MiddleDown = 0x0020;
        const int MouseEvent_MiddleUp = 0x0040;
        const int MouseEvent_RightDown = 0x0008;
        const int MouseEvent_RightUp = 0x0010;
        const int MouseEvent_Wheel = 0x0800;
        const int MousseEvent_XUp = 0x0100;
        const int MousseEvent_XDown = 0x0080;

        [DllImport("user32.dll")]
        public static extern UInt32 SendInput(UInt32 nInputs, Input[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Explicit)]
        public struct Input
        {
            [FieldOffset(0)] public Int32 type;
            [FieldOffset(4)] public MouseInput mi;
            [FieldOffset(4)] public tagKEYBDINPUT ki;
            [FieldOffset(4)] public tagHARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public Int32 dx;
            public Int32 dy;
            public Int32 Mousedata;
            public Int32 dwFlag;
            public Int32 time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct tagKEYBDINPUT
        {
            Int16 wVk;
            Int16 wScan;
            Int32 dwFlags;
            Int32 time;
            IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct tagHARDWAREINPUT
        {
            Int32 uMsg;
            Int16 wParamL;
            Int16 wParamH;
        }

        //获得txt文件的父窗体
        public string className_parent = "Notepad";
        //获得txt文件的编辑框窗体
        public string clsssName_edit = "Edit";
         
        //“另存为"窗口的属性
        public string className_saveAsDialog = "#32770";
        public string dialog_title = "另存为";
        public string className_save = "Button";
        public string saveButtonName = "保存(&S)";

        public static uint FLOODFILLSURFACE = 1;

        //画笔窗口的属性
        public string className_parentPaint = "MSPaintApp";
        public string className_sonPaint = "MSPaintView";
        public string classEditName = "Afx:00007FF628A50000:8";  //每次生成的不一样
        
        

       

        public Form1()
        {
            InitializeComponent();
            this.textBox1.KeyDown += new KeyEventHandler(textBox1_keyDown);
        }

      
        //在隐藏txt文件的前提下，对txt文件写入字符串
        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr hwnd = FindWindow(className_parent, null);
            Console.WriteLine("hwnd的值 " + hwnd);
            ShowWindow(hwnd, 0);
            IntPtr edit = FindWindowEx(hwnd, IntPtr.Zero, clsssName_edit, null);
            Console.WriteLine("edit的值 " + edit);
            if (!edit.Equals(IntPtr.Zero))
            {
                Console.WriteLine("执行之前");
                //调用SendMessage方法设置其内容
                // SetWindowText((int)layer3,"example");
                SendMessage(edit, WM_SETTEXT, IntPtr.Zero, "hehe");

                Console.WriteLine("执行之后");
            }
            Thread.Sleep(3000);
            ShowWindow(hwnd, 1);
 
            }

        //模拟点击文件"另存为"按钮。
        private void button2_Click(object sender, EventArgs e)
        {
            var hWnd = IntPtr.Zero;
            var hChild = IntPtr.Zero;

            // Find Save File Dialog Box
            while (hWnd == IntPtr.Zero)
            {
               // Thread.Sleep(500);
                hWnd = FindWindow(className_saveAsDialog, dialog_title);
            }
          //  IntPtr hwnd = FindWindow(className_saveAsDialog, null);
            Console.WriteLine("hwnd的值 " + hWnd);
             ShowWindow(hWnd, 0);
            Thread.Sleep(2000);
            // IntPtr edit = FindWindowEx(hwnd, IntPtr.Zero, className_save, null);
            hChild = FindWindowEx(hWnd, IntPtr.Zero, className_save, saveButtonName);
            PostMessage(hChild, WM_IME_KEYDOWN, (int)Keys.S, 0);
            PostMessage(hChild, WM_IME_KEYUP, (int)Keys.S, 0);
           ShowWindow(hWnd, 0);
        }

        public int MAKELONG(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }


        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);

        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32", EntryPoint = "mouse_event")]
        private static extern int mouse_event(
        int dwFlags,// 下表中标志之一或它们的组合 
        int dx,
        int dy, //指定x，y方向的绝对位置或相对位置 
       int cButtons,//没有使用 
       int dwExtraInfo//没有使用 
      );

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern int SetCursorPos(int x, int y);

        private int MakeLParam(int LoWord, int HiWord)
        {
            return ((HiWord << 16) | (LoWord & 0xffff));
        }

        //在画布上画出一条线
        private void button3_Click(object sender, EventArgs e)
        {
            IntPtr hwnd = FindWindow(className_parentPaint, null);
            Console.WriteLine("hwnd的值 " + hwnd);

            IntPtr sonDialog = FindWindowEx(hwnd, IntPtr.Zero, className_sonPaint, null);
            Console.WriteLine("sonDialog的值 " + sonDialog);
            IntPtr editOzen = FindWindowEx(sonDialog, IntPtr.Zero, classEditName, null);
            Console.WriteLine("editOzen的值为" + editOzen);
 

            //SetCursorPos(316, 210);
            ShowWindow(hwnd, 0);//1表示显示，0表示隐藏  
            SendMessage(editOzen, WM_LBUTTONDOWN, 0, MakeLParam(316, 210));
            SendMessage(editOzen, WM_MOUSEMOVE, 0, MakeLParam(500, 300));
            SendMessage(editOzen, WM_LBUTTONUP, 0, MakeLParam(500, 300)); 
             ShowWindow(hwnd, 1);//1表示显示，0表示隐藏 
             

        }

        private void textBox1_keyDown(object sender, KeyEventArgs e)
        {
            String txt = textBox1.Text;
            if (e.KeyCode == Keys.Enter)
            {
                if (txt.Equals("123"))
                {
                    Console.WriteLine("success");
                }
                else
                {
                    Console.WriteLine("fail");
                  //  textBox1.Text = "";
                }
                
            }
           
            
            //if (txt.Equals("123"))
            //{
            //    Console.WriteLine("SUCCESS");
            //}
            //else
            //{
            //    Console.WriteLine("FAIL");
            //} 
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk,    //虚拟键值
           byte bScan,// 一般为0
           int dwFlags,  //这里是整数类型  0 为按下，2为释放
           int dwExtraInfo  //这里是整数类型 一般情况下设成为 0
       );
        //函数的目的是想textBox中写入内容，然后回车。
        private void button4_Click(object sender, EventArgs e)
        { 
            String prntClass = "WindowsForms10.Window.8.app.0.141b42a_r10_ad1";
            String prntName = "Form1";

            String passTxtClass = "WindowsForms10.EDIT.app.0.141b42a_r10_ad1";
            String passTxtName = "";
            //获得密码输入框所在的父窗口的句柄
            IntPtr hwnd = FindWindow(prntClass, prntName);
            Console.WriteLine("hwnd的值 " + hwnd);
            //获得密码输入框所在的句柄
            IntPtr sonDialog = FindWindowEx(hwnd, IntPtr.Zero, passTxtClass, null);
            Console.WriteLine("sonDialog的值 " + sonDialog);

           ShowWindow(hwnd, 0);//1表示显示，0表示隐藏  
            //开始写入内容
            SendMessage(sonDialog, WM_SETTEXT, IntPtr.Zero, "123");
             ShowWindow(hwnd, 1);//1表示显示，0表示隐藏 
            textBox1.Focus();
            SendKeys.SendWait("{Enter}");
             ShowWindow(hwnd, 0);//1表示显示，0表示隐藏  
            //SendKeys.SendWait("{Enter}");
            // keybd_event(13,0,0,0);
             //Thread.Sleep(2000);
             //ShowWindow(hwnd, 1);//1表示显示，0表示隐藏  
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

