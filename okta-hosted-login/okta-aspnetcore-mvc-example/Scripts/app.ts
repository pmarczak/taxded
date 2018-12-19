class Main {
	hello: string;

	constructor() {
		this.hello = "Hello!";
	}
}

$().ready(() => {
	var main = new Main();

	ko.applyBindings(main);
});