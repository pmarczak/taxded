var dayStates = ['work', 'holiday', 'vacation'];


$('.calendar .day.active').on('click',
	(element) => {
		
		var $this = $(element.target);

		for (var it = 0; it < dayStates.length; it++) {
			if ($this.hasClass(dayStates[it])) {

				var nextStateIndex = it + 1;

				if (nextStateIndex >= dayStates.length) {
					nextStateIndex = 0;
				}

				$this.removeClass(dayStates[it]);
				$this.toggleClass(dayStates[nextStateIndex]);

				break;
			}
		}



	});