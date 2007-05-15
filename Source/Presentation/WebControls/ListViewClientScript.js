
// Initialises all Atlanta server ListView controls
function InitialiseAtlantaListViewControls()
{
    for (var listViewIndex in _atlantaListViewControls)
    {
        var listView = _atlantaListViewControls[listViewIndex];

        alert(listView.outerHTML);
    }
}



