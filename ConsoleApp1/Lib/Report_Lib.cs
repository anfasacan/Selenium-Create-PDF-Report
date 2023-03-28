using AventStack.ExtentReports;
using OpenQA.Selenium;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Model;
using iText.Html2pdf;
using iText.Html2pdf.Css;
using iText.Html2pdf.Attach.Impl;

using iText.Layout;
using iText.Kernel.Pdf;
using static ConsoleApp1.Lib.Fuction;
using iText.Html2pdf.Css.Apply.Impl;
using System.Xml.Serialization;

namespace ConsoleApp1.Lib
{
    class Report_Lib
    {
        public static ExtentReports extent;
        public static string screenshotPath;
        //public static ExtentReports extent = new ExtentReports();
        public static ITakesScreenshot screenshotDriver = (ITakesScreenshot)WebDriverFactory.Driver;

        public static void ScreenShot(ExtentTest Test_menu, string Description, string Information)
        {
            // Take a screenshot and save it to a file and Add the screenshot to the test report
            Thread.Sleep(2000);
            screenshotPath = Fuction.PathProject() + "/screenshot/screenshot" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".png";
            screenshotDriver.GetScreenshot().SaveAsFile(screenshotPath);
            //Console.WriteLine(Information);
            if (Information == "passed")
            {
                Test_menu.Log(Status.Pass, Description, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
            else if (Information == "failed")
            {
                Test_menu.Log(Status.Fail, Description, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
            else if (Information == "info")
            {
                Test_menu.Log(Status.Info, Description, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
            else if (Information == "warning")
            {
                Test_menu.Log(Status.Warning, Description, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
            else if (Information == "error")
            {
                Test_menu.Log(Status.Error, Description, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
        }
        public static void ExtentReports()
        {
            string reportPath = Fuction.PathProject() + "/Report/" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".html";
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent.AttachReporter(htmlReporter);
        }

        public static void SaveReport()
        {
            // Flush the report to write the test results to the file

        }

        public static void CreatePdfReport() 
        {
            //Read HTML File
            Thread.Sleep(5000);
            if (File.Exists(Fuction.PathProject() + "/Report/" + Fuction.TimeRun + ".html"))
            {
                // Read the entire file into a string and Cosumize it
                string textHTML = File.ReadAllText(Fuction.PathProject() + "/Report/" + Fuction.TimeRun + ".html");
                textHTML = textHTML.Replace("d-none", "");
                textHTML = textHTML.Replace("test-content", "");

                //Create PDF File
                FileStream pdfFile = new FileStream(Fuction.PathProject() + "/Report/" + Fuction.TimeRun + ".pdf", FileMode.Create);
                ConverterProperties properties = new ConverterProperties();
                properties.SetCssApplierFactory(new DefaultCssApplierFactory());

                HtmlConverter.ConvertToPdf(textHTML, pdfFile, properties);

                // Confirm that the file was created and written to
                if (File.Exists(Fuction.PathProject() + "/Report/" + Fuction.TimeRun + ".pdf"))
                {
                    Console.WriteLine("File created successfully.");
                    //File.Move(Fuction.PathProject() + "/Report/" + Fuction.TimeRun + "To PDF.txt", Fuction.PathProject() + "/Report/" + Fuction.TimeRun + "To PDF.html");
                }
                else
                {
                    Console.WriteLine("File creation failed.");
                }

            }
            else
            {
                Console.WriteLine("File not found: " + Fuction.PathProject() + "/Report/" + Fuction.TimeRun + ".html");
            }

           
        }
    }
}
