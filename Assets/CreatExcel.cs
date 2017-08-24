using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using Excel;
using ICSharpCode.SharpZipLib;
using UnityEditor;

public class CreatExcel : MonoBehaviour {
    public List<Texture2D> qrCodes;
    public List<string> result;
	// Use this for initialization
	void Start () {
        // WirteExcel(Application.dataPath + "/Codes.xlsx");
        GetCodeString();
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void GetCodeString()
    {
        //print("Start");
        for (int i = 0;i<qrCodes.Count;i++)
        {
            //批量扫描二维码
            string resultStr = QRCodeDecodeController.DecodeByStaticPic(qrCodes[i]);
            result.Add(resultStr);
            if (i>= qrCodes.Count - 1)
            {
               // print("OK!");

                WirteExcel(Application.dataPath + "/Codes.xlsx");
            }
        }
    }

    /// <summary>
    /// 创建Excel文件并且写入数据
    /// </summary>
    /// <param name="path"></param>
    public void WirteExcel(string path)
    {
       // print("asdasdasd");
        FileInfo newFile = new FileInfo(path);
        if (newFile.Exists)
        {
            newFile.Delete();
            newFile = new FileInfo(path);
           // newfliee = newFile;
        }
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            //添加一个新的sheet
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
            //添加标题
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "XXX";
            worksheet.Cells[1, 3].Value = "Content";
            worksheet.Cells[1, 4].Value = "Name";

            //循环添加数据
            for (int i = 2; i < qrCodes.Count +2; i++)
            {
                worksheet.Cells["A" + i.ToString()].Value = (i-1).ToString();
                worksheet.Cells["B" + i.ToString()].Value = "XXX";
                worksheet.Cells["C" + i.ToString()].Value = result[i - 2];
                worksheet.Cells["D" + i.ToString()].Value = qrCodes[i - 2].name;
            }

            package.Save();

        }
    }
}
