echo on
@nuget pack Maxxor.PCL.nuspec -Version %1 
@nuget push Maxxor.PCL.%1.nupkg 9faaf07d-5c28-486e-a967-d310b386f7a7 -Source https://www.myget.org/F/maxxor/api/v2/package
del Maxxor.PCL.%1.nupkg

