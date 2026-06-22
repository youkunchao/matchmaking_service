#!/bin/bash

echo "======================================"
echo "  郑好遇见你 - Vercel 部署脚本"
echo "======================================"
echo ""

# 检查是否安装了 vercel
if ! command -v vercel &> /dev/null; then
    echo "正在安装 Vercel CLI..."
    npm install -g vercel
fi

echo ""
echo "请按照以下步骤部署："
echo ""
echo "1. 首次使用需要登录 Vercel："
echo "   vercel login"
echo ""
echo "2. 部署企业官网："
echo "   cd /workspace/frontend-website && vercel --prod"
echo ""
echo "3. 部署管理后台："
echo "   cd /workspace/frontend-admin && vercel --prod"
echo ""
echo "4. 部署微信小程序H5："
echo "   cd /workspace/frontend-miniprogram && vercel --prod"
echo ""
echo "======================================"
echo ""
echo "开始自动部署（需要先登录）..."
echo ""

# 尝试部署
read -p "是否已登录 Vercel？(y/n): " logged_in

if [ "$logged_in" = "y" ]; then
    echo ""
    echo "正在部署企业官网..."
    cd /workspace/frontend-website
    vercel --prod --yes
    
    echo ""
    echo "正在部署管理后台..."
    cd /workspace/frontend-admin
    vercel --prod --yes
    
    echo ""
    echo "正在部署微信小程序H5..."
    cd /workspace/frontend-miniprogram
    vercel --prod --yes
    
    echo ""
    echo "======================================"
    echo "  部署完成！"
    echo "======================================"
else
    echo ""
    echo "请先运行: vercel login"
    echo "然后重新执行此脚本"
fi