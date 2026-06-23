#!/bin/bash

echo "======================================"
echo "  郑好遇见你 - 内网穿透启动脚本"
echo "======================================"
echo ""

# 检查是否安装了 localtunnel
if ! command -v lt &> /dev/null; then
    echo "正在安装 localtunnel..."
    npm install -g localtunnel
fi

echo ""
echo "正在启动内网穿透服务..."
echo ""

# 企业官网
echo "启动企业官网穿透 (端口 5173)..."
lt --port 5173 --subdomain zhenghao-matchmaking-website &
WEBSITE_PID=$!

# 管理后台
echo "启动管理后台穿透 (端口 5174)..."
lt --port 5174 --subdomain zhenghao-matchmaking-admin &
ADMIN_PID=$!

# 微信小程序 H5
echo "启动微信小程序穿透 (端口 5175)..."
lt --port 5175 --subdomain zhenghao-matchmaking-miniprogram &
MINIPROGRAM_PID=$!

# 后端 API
echo "启动后端API穿透 (端口 5000)..."
lt --port 5000 --subdomain zhenghao-matchmaking-api &
API_PID=$!

echo ""
echo "======================================"
echo "  内网穿透已启动！"
echo "======================================"
echo ""
echo "访问地址："
echo "  企业官网: https://zhenghao-matchmaking-website.loca.lt"
echo "  管理后台: https://zhenghao-matchmaking-admin.loca.lt"
echo "  小程序H5: https://zhenghao-matchmaking-miniprogram.loca.lt"
echo "  后端API:  https://zhenghao-matchmaking-api.loca.lt"
echo ""
echo "注意：首次访问需要点击页面底部的 'Continue' 按钮"
echo ""
echo "按 Ctrl+C 停止服务"
echo "======================================"

wait
