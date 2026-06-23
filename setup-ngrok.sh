#!/bin/bash
# ngrok 内网穿透启动脚本

echo "正在下载 ngrok..."
# Linux
curl -s https://ngrok-agent.s3.amazonaws.com/ngrok.asc | sudo tee /usr/local/bin/ngrok > /dev/null
sudo chmod +x /usr/local/bin/ngrok

echo "请到 https://ngrok.com 注册获取 authtoken"
echo "注册后运行: ngrok config add-authtoken <YOUR_TOKEN>"
echo ""
echo "然后运行以下命令启动穿透："
echo ""
echo "  ngrok http 5173  # 企业官网"
echo "  ngrok http 5174  # 管理后台"
echo "  ngrok http 5175  # 小程序H5"
echo "  ngrok http 5000  # 后端API"
