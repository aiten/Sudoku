kubectl delete -n student-h-aitenbichler deployment sudokusolveserver
kubectl delete -n student-h-aitenbichler service sudokusolveserver-svc
kubectl delete -n student-h-aitenbichler ingress sudokusolveserver-ingress
kubectl delete -n student-h-aitenbichler pod -l app=sudokusolveserver
kubectl create -f leocloud-deploy-V2.yaml