///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
	var files = GetFiles("./**");
	foreach(var file in files) {
		Information(file.FullPath);
	}
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("5")
.Does(ctx => {
	System.Threading.Thread.Sleep(5000);
});

Task("10")
.IsDependentOn("5")
.Does(ctx => {
	System.Threading.Thread.Sleep(10000);
});

Task("2")
.Does(ctx => {
	System.Threading.Thread.Sleep(2000);
});

Task("Default")
.IsDependentOn("2")
.IsDependentOn("10")
.Does(() => {
	Information("Hello Cake!");
});

RunTarget(target);
