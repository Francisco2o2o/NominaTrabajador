﻿using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayerPresentation.FormNotificaciones
{
    public abstract class Mbox
    {
        public static DialogResult Show(string text)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text))
                result = msgForm.ShowDialog();
            return result;
        }
        public static DialogResult Show(string text, string caption)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text, caption))
                result = msgForm.ShowDialog();
            return result;
        }
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text, caption, buttons))
                result = msgForm.ShowDialog();
            return result;
        }
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text, caption, buttons, icon))
                result = msgForm.ShowDialog();
            return result;
        }
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text, caption, buttons, icon, defaultButton))
                result = msgForm.ShowDialog();
            return result;
        }

        public static DialogResult Show(IWin32Window owner, string text)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text))
                result = msgForm.ShowDialog(owner);
            return result;
        }
        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text, caption))
                result = msgForm.ShowDialog(owner);
            return result;
        }
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text, caption, buttons))
                result = msgForm.ShowDialog(owner);
            return result;
        }
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text, caption, buttons, icon))
                result = msgForm.ShowDialog(owner);
            return result;
        }
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            DialogResult result;
            using (var msgForm = new frmMessageBox(text, caption, buttons, icon, defaultButton))
                result = msgForm.ShowDialog(owner);
            return result;
        }
    }
}
