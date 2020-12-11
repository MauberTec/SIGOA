<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReport.aspx.cs" Inherits="WebApp.frmReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script>
/*
        function printdiv() {
            //Code for adding HTML content to report viwer
            var headstr = "<html><head><title></title></head><body>";
            //End of body tag
            var footstr = "</body></html>";
            //This the main content to get the all the html content inside the report viewer control
            //"ReportViewer1_ctl10" is the main div inside the report viewer
            //controls who helds all the tables and divs where our report contents or data is available
            var newstr = $("#ReportViewer1_ctl09").html();
            //open blank html for printing
            var popupWin = window.open('', '_blank');
            //paste data of printing in blank html page
            popupWin.document.write(headstr + newstr + footstr);
            //print the page and see is what you see is what you get
            popupWin.print();


            return false;
        }
*/
    </script>
</head>


<body>
    <form id="form1" runat="server">
        <div>
<%--            <button id="btnImprimir" onclick="printdiv();" >Imprimir</button>--%>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="True" />
        </div>

    </form>
</body>
</html>
