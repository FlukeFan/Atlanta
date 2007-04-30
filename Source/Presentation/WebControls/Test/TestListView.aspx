<%@
    Page
    language="c#"
    AutoEventWireup="true"
%>

<%@ Register
    TagPrefix="atlanta"
    Namespace="Atlanta.Presentation.WebControls"
    Assembly="Atlanta.Presentation" %>

<script runat="server" language="c#">

    private void Page_Load()
    {
        if (!IsPostBack)
        {
        }
    }

</script>

<html>
    <head>
        <title>Test ListView Harness</title>
        <link
            rel="stylesheet"
            href="<%=Request.ApplicationPath.TrimEnd('/')%>/web/styles/atlanta.css"
            type="text/css"
            />
        <meta http-equiv="Page-Enter" content="progid:DXImageTransform.Microsoft.RandomDissolve(duration=0)" />
        <meta http-equiv="Page-Exit" content="progid:DXImageTransform.Microsoft.RandomDissolve(duration=0)" />
    </head>
    <body>
        <form runat="server">

            <div>testList1:</div>

            <atlanta:ListView
                id="testList1"
                runat="server"
                />

        </form>
    </body>
</html>


