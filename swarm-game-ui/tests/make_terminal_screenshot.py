import subprocess
from PIL import Image, ImageDraw, ImageFont

result = subprocess.run(["mono", "test_balance_logic.exe"], capture_output=True, text=True, cwd=".")
output_lines = result.stdout.rstrip("\n").split("\n")

FONT_PATH = "/usr/share/fonts/truetype/dejavu/DejaVuSansMono.ttf"
FONT_SIZE = 15
font = ImageFont.truetype(FONT_PATH, FONT_SIZE)
font_bold = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSansMono-Bold.ttf", FONT_SIZE)

PAD_X, PAD_Y = 24, 50
LINE_H = 21
BG = (30, 30, 32)
TITLEBAR = (45, 45, 48)
FG = (220, 220, 220)
GREEN = (98, 209, 128)
CYAN = (110, 200, 230)
YELLOW = (230, 200, 100)

tmp_img = Image.new("RGB", (10, 10))
tmp_draw = ImageDraw.Draw(tmp_img)
max_w = max(tmp_draw.textlength(line, font=font) for line in output_lines)
width = int(max_w) + PAD_X * 2
height = PAD_Y + LINE_H * len(output_lines) + PAD_Y // 2

img = Image.new("RGB", (width, height), BG)
draw = ImageDraw.Draw(img)

draw.rectangle([0, 0, width, 34], fill=TITLEBAR)
for i, c in enumerate([(255, 95, 86), (255, 189, 46), (39, 201, 63)]):
    draw.ellipse([16 + i * 22, 12, 16 + i * 22 + 12, 24], fill=c)
draw.text((width / 2 - 110, 8), "aysegul@tusmec: ~/swarm-game-ui/tests",
          font=font, fill=(160, 160, 160))

y = PAD_Y
for line in output_lines:
    if "[OK]" in line:
        color, f = GREEN, font
    elif "TUM TESTLER BASARILI" in line:
        color, f = GREEN, font_bold
    elif line.startswith("==="):
        color, f = CYAN, font_bold
    elif "VehicleSelectionBar" in line or "secildi" in line.lower() or "butonuna" in line:
        color, f = YELLOW, font
    else:
        color, f = FG, font
    draw.text((PAD_X, y), line, font=f, fill=color)
    y += LINE_H

img.save("terminal_screenshot.png")
print("Kaydedildi: terminal_screenshot.png")
