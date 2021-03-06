﻿// /*******************************************************************************
//  * Copyright 2018 Esri
//  *
//  *  Licensed under the Apache License, Version 2.0 (the "License");
//  *  you may not use this file except in compliance with the License.
//  *  You may obtain a copy of the License at
//  *
//  *  http://www.apache.org/licenses/LICENSE-2.0
//  *
//  *   Unless required by applicable law or agreed to in writing, software
//  *   distributed under the License is distributed on an "AS IS" BASIS,
//  *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  *   See the License for the specific language governing permissions and
//  *   limitations under the License.
//  ******************************************************************************/

using Esri.ArcGISRuntime.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esri.ArcGISRuntime.ExampleApps.MapsApp.ViewModels
{
    public class BasemapsViewModel : BaseViewModel
    {
        private IEnumerable<PortalItem> _basemaps;
        private PortalItem _selectedBasemap;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasemapsViewModel"/> class.
        /// </summary>
        public BasemapsViewModel()
        {
            LoadPortal();
        }

        /// <summary>
        /// Property holding the list of basemaps to be added to the UI
        /// </summary>
        public IEnumerable<PortalItem> Basemaps
        {
            get { return _basemaps; }
            set
            {
                if (_basemaps != value && value != null)
                {
                    _basemaps = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the basemap the user selected
        /// </summary>
        public PortalItem SelectedBasemap
        {
            get { return _selectedBasemap; }
            set
            {
                if (_selectedBasemap != value && value != null)
                {
                    _selectedBasemap = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Loads the user specified portal instance
        /// </summary>
        private async Task LoadPortal()
        {
            try
            {
                var portal = await ArcGISPortal.CreateAsync();
                await LoadMaps(portal);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Unable to connect to Portal");
                StackTrace = ex.ToString();
            }
        }

        /// <summary>
        /// Loads basemaps from Portal
        /// </summary>
        private async Task LoadMaps(ArcGISPortal portal)
        {
            try
            {
                var items = await portal.GetBasemapsAsync();
                Basemaps = items?.Select(b => b.Item).OfType<PortalItem>();
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Unable to retrieve basemaps");
                StackTrace = ex.ToString();
            }
        }
    }
}
