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
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Task A")
.Does(ctx => {
	System.Threading.Thread.Sleep(5000);
});

Task("Task B")
.IsDependentOn("Task A")
.Does(ctx => {
	System.Threading.Thread.Sleep(1000);
	Error("Testing arg parsing for target: {0}", target);
});

Task("Task D")
.WithCriteria(() => 1 == 2)
.Does(ctx => {
	Information("lolnope");
});

Task("Task C")
.Does(ctx => {
	System.Threading.Thread.Sleep(2000);
	Warning("Warning from Task C");
});

Task("Default")
.IsDependentOn("Task C")
.IsDependentOn("Task B")
.IsDependentOn("Task D")
.Does(() => {
	var files = GetFiles("./**/*");
	foreach(var file in files) {
		Information(file.FullPath);
	}
});

RunTarget(target);
