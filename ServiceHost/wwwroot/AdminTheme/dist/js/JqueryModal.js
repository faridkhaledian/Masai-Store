
	$(document).ready(function () {
		// نمایش مدال با انیمیشن
		function showModal() {
			$('#MainModal').fadeIn(200).find('.modal-dialog').addClass('showing');
		}

			// مخفی‌سازی مدال
			function hideModal() {
		$('#MainModal').fadeOut(200);
			}

	// وقتی دکمه‌های بستن زده شد
	$('.close, [data-dismiss="modal"]').click(function () {
		hideModal();
			});

	// اگر می‌خواهی با دکمه جداگانه‌ای مدال نمایش داده شود
	$('#showModalBtn').click(function () {
		showModal();
			});
		});