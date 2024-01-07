using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private DTEEvents _dteEventsClass;

        private List<FileCollection> _fileCollections = new List<FileCollection>();

        // TODO: Ermitteln, welche Konfigurationen vorliegen und diese auflisten

        // TODO: Möglichkeit, den aktuellen Zustand zu speichern, Dialog: https://learn.microsoft.com/en-us/visualstudio/extensibility/creating-and-managing-modal-dialog-boxes?view=vs-2022
        // https://www.visualstudiogeeks.com/extensibility/visual%20studio/options-for-displaying-modal-dialogs-in-visual-studio-extensions

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
            _dteEventsClass = _state.DTE.Events.DTEEvents;

            _documentEvents.DocumentSaved += DocumentEventsOnDocumentSaved;
            _documentEvents.DocumentOpened += DocumentEventsOnDocumentOpened;

            _selectionEvents.OnChange += SelectionEventsOnOnChange;

            _solutionEvents.Opened += SolutionOpened;
            // _solutionEvents

            _dteEventsClass.OnStartupComplete += OnStartupComplete;
        }

        private void OnStartupComplete()
        {

        }

        private void SolutionOpened()
        {
           
        }

        private void DocumentEventsOnDocumentSaved(Document document)
        {

        }

        private void SelectionEventsOnOnChange()
        {
           // lstEvents.Items.Add($"Auswahl hat sich geändert {DateTime.Now}");
        }

        private void DocumentEventsOnDocumentOpened(Document document)
        {
            // lstEvents.Items.Add($"Dokument geöffnet {document.Name} {DateTime.Now}");
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
            FileCollectionPropertiesDialog dlg = new FileCollectionPropertiesDialog();

            if (!dlg.ShowModal() == true)
                return;

            var fileCollectionName = dlg.Name;

            string solutionPath = Path.GetDirectoryName(_state.DTE.Solution.FileName).ToLower();

            if (_fileCollections?.Any() ?? true)
            {
                var existingFileCollections = FileCollectionsContainer.Load(solutionPath).ToList();

                _fileCollections = (existingFileCollections?.Any() ?? false) ? existingFileCollections : new List<FileCollection>();
            }

            // eine neue Sammlung erstellen
            FileCollection fc = new FileCollection()
            {
                Name = fileCollectionName,
                Files = new List<string>()
            };

            foreach (Document document in _state.DTE.Documents)
            {
                var documentFileName = document.FullName.ToLower();

                if (documentFileName.StartsWith(solutionPath))
                {
                    documentFileName = documentFileName.Remove(0, solutionPath.Length);
                }
                
                fc.Files.Add(documentFileName);
            }

            _fileCollections.Add(fc);

            FileCollectionsContainer.Save(_fileCollections, solutionPath);

            // Eintrag der Liste hinzufügen
            lstEvents.Items.Add(fc.Name);
        }

        private void LoadButton_OnClick(object sender, RoutedEventArgs e)
        {
            lstEvents.Items.Clear();

            var fc = FileCollectionsContainer.Load(Path.GetDirectoryName(_state.DTE.Solution.FileName).ToLower());

            if (fc != null)
            {
                _fileCollections = fc.ToList();
                foreach (var s in _fileCollections.Select(f => f.Name))
                {
                    lstEvents.Items.Add(s);
                }
            }
        }

        private void LstEvents_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            if (sender is ListBox listBox && listBox.SelectedItem != null && (_fileCollections?.Any() ?? false))
            {
                string itemName = listBox.SelectedItem.ToString();

                // nach dem Element suchen
                var fc = _fileCollections.FirstOrDefault(i => i.Name.Equals(itemName));

                if (fc?.Files?.Any() ?? false)
                {
                    var rootPath = Path.GetDirectoryName(_state.DTE.Solution.FileName);

                    foreach (var f in fc.Files)
                    {
                        var fileName = rootPath + f;

                        if (File.Exists(fileName))
                        {
                            // TODO: Überprüfen, ob das Dokument überhaupt noch Teil der Solution ist

                            _state.DTE.Documents.Open(fileName);
                        }
                    }

                }
            }
        }
    }
}