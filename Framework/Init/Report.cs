using System;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;


namespace Framework.Init
{
    public class Report
    {
        public static StringBuilder sbHtml = null;
        public static StringBuilder sbSummaryHtml = null;
        public static StringBuilder sbTcHtml = null;
        public static StringBuilder sbFeatureHtml = null;
        public static int TCcnt = 1;
        public static int intFeatureCnt = 1;
        public static Int16 IsPassed = 0;
        public static Int16 IsFtrPassed = 0;
        public static bool IsHeaderUpdated = false;
        public static int _intPassedCnt = 0;
        public static int _intFailedCnt = 0;
        public static int _intWarningCnt = 0;
        public static int _inTotalCnt = 0;
        public static DateTime strDtTm = DateTime.Now;
        public static int _intVerificationPassCnt = 0;
        public static int _intVerificationWarningCnt = 0;
        public static int _intVerificationFailCnt = 0;
        /////public static string strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Report\\" + strDtTm.ToString("ddMMMyyyy_HH-mm") + "\\";
        public static string strPath=AppDomain.CurrentDomain.BaseDirectory + "\\Report\\";

        //public Report()
        //{
        //    Console.WriteLine("Rpt Cons Called");
        //    sbHtml = null;
        //    sbSummaryHtml = null;
        //    TCcnt = 1;
        //    IsPassed = true;
        //}

        public static void AddToHtmlReportTCHeader(string strLine)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
            //sbHtml.Append("<h2 style='text-decoration:underline;'>TC" + TCcnt + ":: " + strLine + "</h2>");
            sbHtml.Append("<h2 style='text-decoration:underline;'>::" + strLine + "</h2>");
            TCcnt++;
            IsPassed = 0;
            IsHeaderUpdated = false;
            intFeatureCnt = 1;
            IsFtrPassed = 0;
            _intVerificationPassCnt = 0;
            _intVerificationWarningCnt = 0;
            _intVerificationFailCnt = 0;
            //Console.WriteLine("1TCcnt - 1 :" + (TCcnt - 1));
            /////strDtTm = DateTime.Now;
            /////strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Report\\" + strDtTm.ToString("ddMMMyyyy_HH-mm") + "\\";
        }
        public static void AddToHtmlReportPassed(string strLine, bool IsItalic = false)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
           // sbHtml.Append("<div class='passed'> " + Common.highlightQuotedHTML(strLine) + " <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div><br>");
            sbHtml.Append("<div class='passed'>"+strLine+" <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div><br>");
           
            if (IsPassed != 2 && IsPassed != 3) IsPassed = 1;
            if (IsFtrPassed != 2 && IsFtrPassed != 3) IsFtrPassed = 1;
            _intVerificationPassCnt++;
            //Console.WriteLine("2TCcnt - 1 :" + (TCcnt - 1));
        }
        public static void AddToHtmlReportOrange(string strLine, bool IsItalic = false)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
            //sbHtml.Append("<div class='orange'> " + Common.highlightQuotedHTML(strLine) + " <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div><br>");
            sbHtml.Append("<div class='orange'>"+strLine+" <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div><br>");
          
            if (IsPassed != 2) IsPassed = 3;
            if (IsFtrPassed != 2) IsFtrPassed = 3;
            _intVerificationWarningCnt++;
        }
        public static void AddToHtmlReportFailed(IWebDriver driver, Exception ex, string strLine, bool IsItalic = false)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
            sbHtml.Append("<table cellpadding='2' cellspacing='0'><tr><td style='width:5%;white-space: nowrap;' >");
            //sbHtml.Append("<div class='failed'> " + Common.highlightQuotedHTML(strLine) + " <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div>");
            sbHtml.Append("<div class='failed'>"+strLine+"<span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div>");
            

            sbHtml.Append("</td><td>");
            sbHtml.Append("<div class='dvSnap'> [<a href='" + GetScreenShot(driver) + "' target='_blank'> <strong>View Snap</strong> </a>] </div>");
            sbHtml.Append("</td></tr></table>");
            sbHtml.Append("<div class='errTrace' >Error Message: " + ex.Message + "</div>");
            IsPassed = 2;
            IsFtrPassed = 2;
            _intVerificationFailCnt++;
            //Console.WriteLine("3TCcnt - 1 :" + (TCcnt - 1));
        }
        public static void AddToHtmlReportFeatureFinish()
        {
            try
            {
                if (sbHtml == null) sbHtml = new StringBuilder();
                sbHtml.Append("</div></div></div>");

                //Console.WriteLine("4TCcnt - 1 :" + (TCcnt - 1));
                /*if (IsPassed == 1)
                {
                    //Console.WriteLine("1111111111111");
                    if (!sbHtml.ToString().Contains("<div id='TC" + (TCcnt - 1) + "' class='passed'"))
                    {
                        //Console.WriteLine("88888888888888");
                        sbHtml.Insert(((sbHtml.ToString().IndexOf("::"))), "<div id='TC" + (TCcnt - 1) + "' class='passed' style='float:left; height:16px; margin-top:2px;' ></div>");
                    }
                    
                    IsHeaderUpdated = true;
                }
                else if (IsPassed == 2)
                {
                    //Console.WriteLine("7777777777777");

                    IsHeaderUpdated = true;
                    if (sbHtml.ToString().Contains("<div id='TC" + (TCcnt - 1) + "' class='passed'"))
                    {
                        //Console.WriteLine("22222222222222");
                        sbHtml.Replace("<div id='TC" + (TCcnt - 1) + "' class='passed'", "<div id='TC" + (TCcnt - 1) + "' class='failed'");
                    }
                    else if (sbHtml.ToString().Contains("<div id='TC" + (TCcnt - 1) + "' class='failed'"))
                    {
                        //Console.WriteLine("333333333333333");
                        sbHtml.Replace("<div id='TC" + (TCcnt - 1) + "' class='passed'", "<div id='TC" + (TCcnt - 1) + "' class='failed'");
                    }
                    else sbHtml.Insert(((sbHtml.ToString().IndexOf("::"))), "<div id='TC" + (TCcnt - 1) + "' class='failed' style='float:left; height:16px; margin-top:2px;' ></div>");
                    //Console.WriteLine("66666666666666");


                }*/
                if (IsFtrPassed == 1)
                {
                    //Console.WriteLine("4444444444444444");
                    sbHtml.Replace("<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption'", "<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption squareboxcaptionP'");
                    sbHtml.Replace("(())", "");
                }
                else if (IsFtrPassed == 2)
                {
                    //Console.WriteLine("5555555555555555");
                    sbHtml.Replace("<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption'", "<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption squareboxcaptionF'");
                    sbHtml.Replace("(())", "(" + _intVerificationFailCnt+ ")");
                }
                else if (IsFtrPassed == 3)
                {
                    // Console.WriteLine("5555555555555555");
                    sbHtml.Replace("<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption'", "<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption squareboxcaptionO'");
                    sbHtml.Replace("(())", "(" + _intVerificationWarningCnt+ ")");
                }
                _intVerificationPassCnt = 0;
                _intVerificationWarningCnt = 0;
                _intVerificationFailCnt = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            
        }

        public static void AddToHtmlReport(string strLine, bool IsHeader = false, bool IsBold = false, bool IsUnderline = false, bool IsItalic = false)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
            string strHtml = string.Empty;
            if (IsHeader)
            {
                strHtml += "<div style='clear:both; '>";
                strHtml += "<div class='squarebox'>";
                strHtml += "<div id='dvFeature" + intFeatureCnt + "TC" + (TCcnt-1)+ "' class='squareboxcaption' onclick='togglePannelStatus(this.nextSibling)'>";
                strHtml += "<div style='padding: 10px 10px 10px 5px; float: left;'>";
                strHtml += "";
                strHtml += "<strong>" + strLine + "</strong> <span class='VrTimeStmp' > (())</span><br>";
                strHtml += "</div>";
                strHtml += "<div class='upDownArrow' >";
                strHtml += "<img src='../images/expand.gif' width='13' height='14' border='0' alt='Show/Hide' title='Show/Hide' /> </div>";
                strHtml += "</div>";
                strHtml += "<div class='squareboxcontent' style='display: none;clear:both;'>";
                sbHtml.Append(strHtml + "<br>");
                intFeatureCnt++;
                IsFtrPassed = 0;
            }
            else
            {
                if (IsBold)
                {
                    strLine = "<strong>" + strLine + "</strong>";
                }
                if (IsUnderline)
                {
                    strLine = "<u>" + strLine + "</u>";
                }
                if (IsItalic)
                {
                    strLine = "<i>" + strLine + "</i>";
                }
                sbHtml.Append(strLine + "<br>");
            }

        }
        //private static readonly object syncLock = new object();
        public static void GenerateHtmlReport(string strReportHeader = "")
        {

            //lock (syncLock)
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                //strPath = strPath + "\\" + strDtTmStamp + "\\";
                //Directory.CreateDirectory(strPath);

                string strFilePath = strPath + "Report.html";

                StringBuilder lines = new StringBuilder();
                System.IO.StreamWriter file = new System.IO.StreamWriter(strFilePath);
                file.Write("<html><head><title>Test Report</title><style type='text/css'> body { background-color:#C0C0C0; color:#3D3D3D; font-size: 12px; font-family:Arial, Courier; padding-left:7px;} h2{margin:23px 0px 4px 0px; font-weight:normal;} hr{border:1px solid lightgray;} table {font-size: 12px; font-family:Arial, Courier; } .dvSnap{margin: -4px 0px -8px -2px; padding: 5px 0px 0px 20px;} .passed {color: #006600; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/passed.png') no-repeat top left; } .failed { color: #CC0000; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/failed.png') no-repeat top left;} a{text-decoration:none} a:hover{text-decoration:underline;} .errTrace {font: 12px serif; padding:5px; margin: 10px 0px 0px 5px; color: #AE0000; background-color: #FFEFC6; border:dashed 1px #AE0000; border-radius: 4px;} .clientHeaderLogo{background: url('../Images/ExpertoryLogo.png') no-repeat top left; width:40%;} .legendPassed{width:20px; background: url('../Images/passed.png') no-repeat top left;} .legendFailed{width:20px; background: url('../Images/failed.png') no-repeat top left;} .legendInfo{width:20px; background: url('../Images/info.png') no-repeat top left;} .squarebox {width: 100%; overflow: hidden; } .squareboxcaption { margin-top: 5px; margin-left: 5px; float: left; cursor: pointer;} .squareboxcaptionP {background-color: #D2EFD1; border: 1px solid #6F9F6D; } .squareboxcaptionF {background-color: #FFCFCF; border: 1px solid #CF3333;} .squareboxcontent { border:1px solid lightgray; margin-left:5px; padding: 0px 10px; overflow: hidden; } .upDownArrow{float: left; padding: 10px 10px 11px 10px; vertical-align: middle; border-left: 1px dotted #FFFFFF;} iframe{border-top:0px none; border-right:0px none; border-bottom:1px solid gray; border-left:0px none;overflow:auto; display:block; visibility:visible; margin-bottom: 10px; height:200px; width: 99%;} .dvRptFrame{ background-color: #FFFFFF; border: 1px solid gray; border-radius:10px 10px 10px 10px; margin: 2px 15px 30px 15px; padding: 10px; overflow:auto;min-height:auto; } .dvRptHeader{ height:85px; background-color: #FFFFFF; border: 1px solid gray; border-radius:10px 10px 10px 10px; margin: 10px 15px 2px 15px; padding: 5px 15px; } .dvKiwiQaLogo{ background: url('../Images/TechSageLogo.png') no-repeat top left; height:85px; width:63%; text-align:right; font-size: 18px; float:left; } .dvClientLogo{ background: url('../Images/ClientLogo.png') no-repeat left; height:85px; width:193px; float:right; } .VrTimeStmp{color:#696969;font-size:10px;font-style:italic;font-weight:normal;} </style> <script language='javascript' type='text/javascript'> function togglePannelStatus(content) { var expand = (content.style.display == 'none'); content.style.display = (expand ? 'block' : 'none'); var chevron = content.parentNode .firstChild.childNodes[1].childNodes[0]; chevron.src = chevron.src .split(expand ? 'expand.gif' : 'collapse.gif') .join(expand ? 'collapse.gif' : 'expand.gif'); } </script></head><body>");

                file.Write("<div class='dvRptHeader'><div class='dvKiwiQaLogo' ><h1>NIKO - Automation Test Report</h1></div><div class='dvClientLogo' ></div></div>");
                file.Write("<div class='dvRptFrame'>");

                file.Write("<iframe src='summary.html'>&nbsp;</iframe> ");
                file.Write("<h2 style='text-align:center; border-bottom: 1px dotted lightgray;font-weight:bold;' >Test Result Details</h2> <div style='border:2px solid lightgray; border-radius: 5px; text-align:left; width:120px; padding:3px; margin-right:20px; float:right;'><table cellpadding='3' cellspacing='0'><tr><td class='legendPassed'></td><td>Passed</td></tr><tr><td class='legendFailed'></td><td>Failed</td></tr><tr><td class='legendInfo'></td><td>Information</td></tr></table></div><div style='float:left;'><strong> &nbsp;Date:</strong> " + String.Format("{0:dd/MM/yyyy}", strDtTm /*DateTime.Now*/) + " " + strDtTm.ToString("HH:mm:ss") + "</div><br style='clear:left;' />");
                //file.Write("<h2>" + strReportHeader + "<br>");
                //file.Write("------------------------------------------------------------- </h2>");
                file.WriteLine(sbHtml.ToString());
                file.WriteLine("<br style='clear:both;'><br><br><hr><div style='text-align:center; font-style:italic' >***** Report Ends Here *****</div><br><br>");
                file.WriteLine("</div></body></html>");
                file.Close();
            }
        }


        public static String NewGuid()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "");
        }
        public static String NewFileName()
        {
            return NewGuid() + ".png";
        }
        public static string GetScreenShot(IWebDriver driver)
        {
            string retVal = NewFileName();
            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                
                //strPath = strPath + "\\" + strDtTmStamp + "\\";
                //Directory.CreateDirectory(strPath);
                ss.SaveAsFile(strPath + "\\" + retVal, System.Drawing.Imaging.ImageFormat.Png);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.StackTrace);
            } 
            return retVal;

            
            /*Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            bitmap.Save("c:\\screenshot.jpeg", ImageFormat.Jpeg);*/

            /*ScreenCapture sc = new ScreenCapture();
            // capture entire screen, and save it to a file
            Image img = sc.CaptureScreen();
            // display image in a Picture control named imageDisplay
            this.imageDisplay.Image = img;
            // capture this window, and save it
            sc.CaptureWindowToFile(this.Handle, "C:\\temp2.gif", ImageFormat.Gif);*/

            //string retVal = NewFileName();
            //try
            //{
            //    Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            //    // Create a graphics object from the bitmap.
            //    Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            //    // Take the screenshot from the upper left corner to the right bottom corner.
            //    gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            //    // Save the screenshot to the specified path that the user has chosen.
                
            //    if (!Directory.Exists(strPath))
            //    {
            //        Directory.CreateDirectory(strPath);
            //    }
            //    bmpScreenshot.Save(strPath + "\\" + retVal, ImageFormat.Png);
            //    Console.WriteLine("SSC");
            //}
            //catch (Exception)
            //{
            //   Console.WriteLine("SSC Error ************************ ");
            //}
            //return retVal;
        }

        public static void GenerateHtmlSummaryReport()
        {
            //lock (syncLock)
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                //strPath = strPath + "\\" + strDtTmStamp + "\\";
                //Directory.CreateDirectory(strPath);

                string strFilePath = strPath + "summary.html";

                StringBuilder lines = new StringBuilder();
                System.IO.StreamWriter file = new System.IO.StreamWriter(strFilePath);
                file.Write("<html><head><title>Test Report</title><style type='text/css'> body { color:#3D3D3D; font-size: 12px; font-family:Arial, Courier; } h2{margin:5px 0px 0px 0px;} hr{border:1px solid lightgray;} table {font-size: 12px; font-family:Arial, Courier; } .passed {color: #006600; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/passed.png') no-repeat top left; } .orange {color: #006600; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/passed.png') no-repeat top left; } .failed { color: #CC0000; font-weight:bold; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/failed.png') no-repeat top left;} a{text-decoration:none} a:hover{text-decoration:underline;} .tblSummary{border:2px solid lightgray; border-radius: 5px 5px 5px 5px} .tblSummary th{border-bottom:1px solid lightgray; background-color: silver;} .tdSummary{ text-align:right; padding-right:15px; width:100px;} .tdBleft{border-left:1px solid lightgray; } .tdBbottom{border-bottom:1px solid lightgray;}</style> </head><body><div style='text-align: center;'><h2>Test Result Summary</h2>");
                file.Write("<table cellpadding='4' cellspacing='0' class='tblSummary' align='center' >");
                file.Write("<tr><th style='width:250px;' class='tdBbottom' >Test Name</th><th class='tdBleft tdBbottom' > Passed</th><th class='tdBleft tdBbottom'> Failed </th><th class='tdBleft tdBbottom' > Warning</th><th class='tdBleft tdBbottom'> Total </th></tr>");
                file.WriteLine(sbSummaryHtml.ToString());
                file.Write("</table>");
                file.WriteLine("</body></html>");
                file.Close(); 
            }
        }

        public static void AddToHtmlSummaryReport(string strTestName, int intPassedCnt = 0, int intFailedCnt = 0, int intWarningCnt = 0)
        {
            if (sbSummaryHtml == null) sbSummaryHtml = new StringBuilder();
            if (intPassedCnt > 0 || intFailedCnt > 0 || intWarningCnt > 0)
            {
                _intPassedCnt += intPassedCnt;
                _intFailedCnt += intFailedCnt;
                _intWarningCnt += intWarningCnt;
                _inTotalCnt += (intPassedCnt + intFailedCnt + intWarningCnt);
                sbSummaryHtml.Append("<tr><td class='tdBbottom' >");
                sbSummaryHtml.Append(strTestName);
                sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' >");
                sbSummaryHtml.Append(intPassedCnt);
                sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' >");
                sbSummaryHtml.Append(intFailedCnt);
                sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' >");
                sbSummaryHtml.Append(intWarningCnt);
                sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
                sbSummaryHtml.Append(intPassedCnt + intFailedCnt + intWarningCnt);
                sbSummaryHtml.Append("</td></tr>");
            }
            
        }

        public static void AddToHtmlSummaryReportTotal()
        {
            sbSummaryHtml.Append("<tr><td class='tdBbottom' style='font-weight:bold;' >");
            sbSummaryHtml.Append("Total");
            sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
            sbSummaryHtml.Append(_intPassedCnt);
            sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
            sbSummaryHtml.Append(_intFailedCnt);
            sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
            sbSummaryHtml.Append(_intWarningCnt);
            sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
            sbSummaryHtml.Append(_inTotalCnt);
            sbSummaryHtml.Append("</td></tr>");
        }

    }

    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public ScreenShotRemoteWebDriver(Uri RemoteAdress, ICapabilities capabilities)
            : base(RemoteAdress, capabilities)
        {
        }

        /// <summary>
        /// Gets a <see cref="Screenshot"/> object representing the image of the page on the screen.
        /// </summary>
        /// <returns>A <see cref="Screenshot"/> object containing the image.</returns>
        public Screenshot GetScreenshot()
        {
            // Get the screenshot as base64.
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();

            // ... and convert it.
            return new Screenshot(base64);
        }
    }

    //###########################################################################################################################################################
    /*
    public class ScreenCapture
    {
        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns></returns>
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }
        /// <summary>
        /// Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
        /// <summary>
        /// Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
    }
    */
}
