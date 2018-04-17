using System.Windows.Forms;

namespace Quantum.TriangleProblemProject {
	/// <summary>
	/// A panel with double buffering enabled, to reduce flicker.
	/// </summary>
	public class DoubleBufferedPanel : Panel {
		public DoubleBufferedPanel() {
			DoubleBuffered = true;
		}
	}
}
