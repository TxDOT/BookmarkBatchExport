﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;

namespace BookmarkBatchExport
{
    internal class ExportAllBookmarks : Button
    {
        protected override void OnClick()
        {
            QueuedTask.Run(() =>
            {
                try
                {
                    //Get the active map view.
                    var mapView = MapView.Active;

                    //Return the collection of bookmarks for the map.
                    var bookmarks = mapView.Map.GetBookmarks();

                    //Get the project location and create new folder there
                    var projectPath = Project.Current.HomeFolderPath + "\\";
                    var subdir = System.IO.Directory.CreateDirectory(projectPath + "bookmarks");
                    var fullPath = subdir.FullName + "\\";

                    //Loop through bookmarks and write each to file
                    foreach (var bkm in bookmarks)
                    {
                        mapView.Map.ExportBookmarks(fullPath + bkm.Name + ".bkmx");
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Whoops! Something Went Wrong");
                }
                
            });
        }
    }
}
