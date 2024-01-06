﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using EnvDTE;
using ZimmysDevelopmentNuggets.Components;

namespace ZimmysDevelopmentNuggets.Toolboxes.DocumentManagement
{
    /// <summary>
    /// Interaction logic for ClassViewerToolboxControl.
    /// </summary>
    public partial class DocumentManagementToolboxControl : UserControl
    {
        private ClassViewerState _state;
        private DocumentEvents _documentEvents;
        private SelectionEvents _selectionEvents;
        private SolutionEvents _solutionEvents;

        // TODO: Ermitteln, welche Konfigurationen vorliegen und diese auflisten

        // TODO: Möglichkeit, den aktuellen Zustand zu speichern

        // TODO: Möglichkeit, den aktuellen Zustand wieder herzustellen (ersetzen oder ergänzen)

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassViewerToolboxControl"/> class.
        /// </summary>
        public DocumentManagementToolboxControl(ClassViewerState state)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            this.InitializeComponent();

            _state = state;

            _documentEvents = _state.DTE.Events.DocumentEvents;
            _selectionEvents = _state.DTE.Events.SelectionEvents;
            _solutionEvents = _state.DTE.Events.SolutionEvents;

            _documentEvents.DocumentSaved += DocumentEventsOnDocumentSaved;
            _documentEvents.DocumentOpened += DocumentEventsOnDocumentOpened;

            _selectionEvents.OnChange += SelectionEventsOnOnChange;

            _solutionEvents.Opened += SolutionOpened;
        }

        private void SolutionOpened()
        {
           
        }

        private void DocumentEventsOnDocumentSaved(Document document)
        {

        }

        private void SelectionEventsOnOnChange()
        {
            lstEvents.Items.Add($"Auswahl hat sich geändert {DateTime.Now}");
        }

        private void DocumentEventsOnDocumentOpened(Document document)
        {
            lstEvents.Items.Add($"Dokument geöffnet {document.Name} {DateTime.Now}");
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            lstEvents.Items.Clear();

            List<FileCollection> fileCollections = new List<FileCollection>();

            string solutionPath = _state.DTE.Solution.FileName.ToLower();
            
            FileCollection fc = new FileCollection()
            {
                Name = DateTime.Now.ToString(),
                Files = new List<string>()
            };

            foreach (Document document in _state.DTE.Documents)
            {
                //lstEvents.Items.Add($"Dokument '{document.Name}' '{document.FullName}' '{document.Path}'");

                var documentFileName = document.FullName.ToLower();

                if (documentFileName.StartsWith(solutionPath))
                {
                    documentFileName = documentFileName.Remove(0, solutionPath.Length);
                }
                
                fc.Files.Add(documentFileName);
            }

            fileCollections.Add(fc);

            FileCollectionsContainer.Save(fileCollections, _state.DTE.Solution.FileName);
        }
    }
}