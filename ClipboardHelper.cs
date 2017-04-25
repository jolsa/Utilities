using System;
using System.Linq;
using System.Windows.Forms;

namespace Utilities
{
	public static class ClipboardHelper
	{
		public static void CopyHtml(string html, string style = null)
		{
			const int startPoint = 89;
			const int htmlLength = 102;
			const int fragmentOffset = 68;

			int styleLength = style?.Length ?? 0;

			var points = new
			{
				startHtml = startPoint,
				endHtml = startPoint + htmlLength + html.Length + styleLength,
				startFragment = startPoint + fragmentOffset + styleLength,
				endFragment = startPoint + fragmentOffset + html.Length + styleLength + 1
			};

			string htmlTemplate = $@"Version:1.0
StartHTML:{points.startHtml:000000}
EndHTML:{points.endHtml:000000}
StartFragment:{points.startFragment:000000}
EndFragment:{points.endFragment:000000}
<HTML>
<head>
<style>{style}</style>
</head>
<body>
<!–StartFragment–>{html}<!–EndFragment–>
</body>
</html>";

			Clipboard.SetText(htmlTemplate, TextDataFormat.Html);
		}
	}
}
