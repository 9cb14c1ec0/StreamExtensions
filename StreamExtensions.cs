/*
Copied almost directly from AE.NET.Mail, so:

Copyright © 2021 Andy Edinborough
Copyright © 2021 Edwin Zimmerman

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated
documentation files (the “Software”), to deal in the Softwarew
ithout restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to
whom the Software is furnished to do so, subject to the
following conditions:The above copyright notice and this permission notice shall
be included in all copies or substantial portions of the
Software.THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY
KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.IO;
using System.Text;
using System;

namespace StreamExtensions
{

    public static class StreamExtensions
    {
       public static string ReadLine(this Stream stream, ref int maxLength, Encoding encoding, char? termChar, int ReadTimeout = 10000) {
			if (stream.CanTimeout)
				stream.ReadTimeout = ReadTimeout;

			var maxLengthSpecified = maxLength > 0;
			int i;
			byte b = 0, b0;
			var read = false;
			using (var mem = new MemoryStream()) {
				while (true) {
					b0 = b;
					i = stream.ReadByte();
					if (i == -1) break;
					else read = true;

					b = (byte)i;
					if (maxLengthSpecified) maxLength--;

					if (maxLengthSpecified && mem.Length == 1 && b == termChar && b0 == termChar) {
						maxLength++;
						continue;
					}

					if (b == 10 || b == 13) {
						if (mem.Length == 0 && b == 10) {
							continue;
						} else break;
					}

					mem.WriteByte(b);
					if (maxLengthSpecified && maxLength == 0)
						break;
				}

				if (mem.Length == 0 && !read) return null;
				return encoding.GetString(mem.ToArray());
			}
		}

		public static string ReadToEnd(this Stream stream, int maxLength, Encoding encoding) {
			if (stream.CanTimeout)
				stream.ReadTimeout = 10000;

			int read = 1;
			byte[] buffer = new byte[8192];
			using (var mem = new MemoryStream()) {
				do {
					var length = maxLength == 0 ? buffer.Length : Math.Min(maxLength - (int)mem.Length, buffer.Length);
					read = stream.Read(buffer, 0, length);
					mem.Write(buffer, 0, read);
					if (maxLength > 0 && mem.Length == maxLength) break;
				} while (read > 0);

				return encoding.GetString(mem.ToArray());
			}
		}
    }
}