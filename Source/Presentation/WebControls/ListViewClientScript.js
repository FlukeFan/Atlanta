

// onselectstart - disables selection by dragging in a list
function AtlantaListView_OnSelectStart()
{
    event.returnValue = false;
    event.cancelBubble = true;
}


// Initialises all Atlanta server ListView controls
function InitialiseAtlantaListViewControls()
{
    for (var listViewIndex in _atlantaListViewControls)
    {
        var listViewName = _atlantaListViewControls[listViewIndex];
        var listView = document.getElementById(listViewName);

        listView.onselectstart = AtlantaListView_OnSelectStart;
    }
}



