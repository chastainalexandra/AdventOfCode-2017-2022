// borrowed from https://github.com/encse/adventofcode

using System;
using System.Text;

namespace AdventOfCode.Generator;
 class BufferWriter {
        StringBuilder sb = new StringBuilder();
        int bufferColor = -1;
        string buffer = "";
        bool bufferBold;

        public void Write(int color, string text, bool bold) {
            if (!string.IsNullOrWhiteSpace(text)) {
                if (!string.IsNullOrWhiteSpace(buffer) && (color != bufferColor || this.bufferBold != bold) ) {
                    Flush();
                }
                bufferColor = color;
                bufferBold = bold;
            }
            buffer += text;
        }

        private void Flush() {
            while (buffer.Length > 0) {
                var block = buffer.Substring(0, Math.Min(100, buffer.Length));
                buffer = buffer.Substring(block.Length);
                block = block.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n");
                sb.AppendLine($@"Write(0x{bufferColor.ToString("x")}, {bufferBold.ToString().ToLower()}, ""{block}"");");
            }
            buffer = "";
        }

        public string GetContent() {
            Flush();
            return sb.ToString();
        }
    }