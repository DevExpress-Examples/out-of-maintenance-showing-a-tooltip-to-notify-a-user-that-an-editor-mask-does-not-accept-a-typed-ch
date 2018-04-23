using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Popup;
using System.Globalization;
using DevExpress.Data.Mask;

namespace T223817 {
    [UserRepositoryItem("RegisterCustomEdit")]
    public class RepositoryItemCustomEdit : RepositoryItemTextEdit {
        private static readonly object invalidCharacter = new object();
        static RepositoryItemCustomEdit() {
            RegisterCustomEdit();
        }

        public const string CustomEditName = "CustomEdit";
        public RepositoryItemCustomEdit() {
        }

        public override string EditorTypeName {
            get { return CustomEditName; }
        }

        public static void RegisterCustomEdit() {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(CustomEdit), typeof(RepositoryItemCustomEdit), typeof(TextEditViewInfo), new TextEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item) {
            RepositoryItemCustomEdit source = item as RepositoryItemCustomEdit;
            BeginUpdate();
            try {
                base.Assign(item);
                if (source == null) {
                    return;
                }
            }
            finally {
                EndUpdate();
            }
            Events.AddHandler(invalidCharacter, source.Events[invalidCharacter]);
        }

        public event InvalidCharacterEventHandler InvalidCharacter {
            add { this.Events.AddHandler(invalidCharacter, value); }
            remove { this.Events.RemoveHandler(invalidCharacter, value); }
        }

        protected internal virtual void RaiseInvalidCharacter(InvalidCharacterEventArgs e) {
            InvalidCharacterEventHandler handler = (InvalidCharacterEventHandler)this.Events[invalidCharacter];
            if (handler != null) {
                handler(GetEventSender(), e);
            }
        }
    }

    public delegate void InvalidCharacterEventHandler(object sender, InvalidCharacterEventArgs e);
    public class InvalidCharacterEventArgs : EventArgs {
        public string InsertionString { get; private set; }
        public bool Handled { get; set; }
        public InvalidCharacterEventArgs(string insertionString) {
            InsertionString = insertionString;
        }
    }

    [ToolboxItem(true)]
    public class CustomEdit : TextEdit {
        static CustomEdit() {
            RepositoryItemCustomEdit.RegisterCustomEdit();
        }

        public CustomEdit() {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomEdit Properties {
            get { return base.Properties as RepositoryItemCustomEdit; }
        }

        public override string EditorTypeName {
            get { return RepositoryItemCustomEdit.CustomEditName; }
        }

        private NumericMaskManager fManager;
        protected override MaskManager CreateMaskManager(DevExpress.XtraEditors.Mask.MaskProperties mask) {
            fManager = (NumericMaskManager)base.CreateMaskManager(mask);
            return fManager;
        }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e) {
            base.OnKeyPress(e);
            string insertion = KeyCharToInsertableString(e.KeyChar);
            if (insertion == null) {
                return;
            }
            if (fManager.Insert(insertion)) {
                fManager.Backspace();
                return;
            }
            ProcessInvalidCharacter(insertion);
        }

        private void ProcessInvalidCharacter(string insertion) {
            InvalidCharacterEventArgs args = new InvalidCharacterEventArgs(insertion);
            this.Properties.RaiseInvalidCharacter(args);
            if (args.Handled) {
                return;
            }
            this.ToolTipController.ShowBeak = true;
            this.ToolTipController.ShowHint("An editor can accept only numbers or maximum length achieved", this.PointToScreen(Point.Empty));
        }

        private string KeyCharToInsertableString(char keyChar) {
            if (!char.IsControl(keyChar)) {
                return keyChar.ToString(CultureInfo.InvariantCulture);
            }
            if (keyChar == '\r' && this.MaskBox.Multiline && this.MaskBox.AcceptsReturn) {
                return "\r\n";
            }
            if (keyChar == '\t' && this.MaskBox.Multiline && this.MaskBox.AcceptsTab) {
                return "\t";
            }
            return null;
        }
    }
}
