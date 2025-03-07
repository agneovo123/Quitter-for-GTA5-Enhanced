using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitter_4_Enhanced
{
    class HotkeyHandler
    {
        public static void HandleHotkeyTextBox(KeyEventArgs e, string name)
        {
            // Build the key combination string
            StringBuilder keyCombo = new StringBuilder();

            // Check for modifier keys
            if (e.Control)
                keyCombo.Append("Ctrl+");
            if (e.Alt)
                keyCombo.Append("Alt+");
            if (e.Shift)
                keyCombo.Append("Shift+");

            // Avoid appending the modifier key itself if it is the only key pressed.
            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Menu && e.KeyCode != Keys.ShiftKey)
            {
                keyCombo.Append(e.KeyCode.ToString());
            }



            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            // "CombinedModifiers"


            // Display the key combination in the textbox
            if (name == "SOLO")
            {
                Form1.form.textBox_SoloKey.Text = keyCombo.ToString();
            }
            if (name == "KILL")
            {
                Form1.form.textBox_KillGame.Text = keyCombo.ToString();
            }
            if (name == "NET")
            {
                Form1.form.textBox_DropNetwork.Text = keyCombo.ToString();
            }

            // Optionally, prevent the default behavior
            e.SuppressKeyPress = true;
            e.Handled = true;
        }
    }
}
