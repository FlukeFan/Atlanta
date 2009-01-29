
try
{
    var iisApp = GetObject("IIS://localhost/w3svc/1/root/atlanta");
    iisApp.AppUnload();
}
catch (e)
{
    WScript.StdErr.WriteLine("Exception caught");

    for (p in e)
        WScript.StdErr.WriteLine(p + "=" + e[p]);

    WScript.Quit(1);
}
