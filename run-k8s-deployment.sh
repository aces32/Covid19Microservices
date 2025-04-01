#!/bin/bash

echo "📦 Applying Volumes..."
kubectl apply -f K8S/volumes/

echo "🚀 Deploying Services..."
kubectl apply -f K8S/deployments/

echo "🌐 Applying Service Resources..."
kubectl apply -f K8S/services/

echo "✅ Done!"
