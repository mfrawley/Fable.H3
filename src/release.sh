VERSION=`cat VERSION`

CMD="dotnet nuget push src/bin/Debug/Fable.H3.$VERSION.nupkg --api-key $NUGET_KEY --source https://api.nuget.org/v3/index.json"
res=`$CMD`
echo $res
