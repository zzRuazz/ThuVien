class NumberProvider {
    static GetDisplayNumber(value) {
        if (!value && value !== 0) return "";
        return value.toString().replace(/\,/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    static GetIntegerNumberFromInput(input) {
        var $this = $(input);
        return $this.val($this.val().replace(/[^0-9]+$/g, ''));
    }

    static GetDoubleNumberFromInput(input) {
        var $this = $(input);
        var newValue = $this.val().replace(/[^0-9\.]+$/g, '');

        var dotCount = 0;
        var result = "";
        for (var index = 0; index < newValue.length; index++) {
            var valueItem = newValue[index];
            if (valueItem === '.') {
                dotCount++;
                if (dotCount > 1) {
                    continue;
                }
            }
            result += newValue[index];
        }

        $this.val(result);
    }

    static Inits() {
        $('body').on('keyup', '[data-control="1"][data-input-type="number"][data-type="double"]', function () {
            let $input = $(this);
            NumberProvider.GetDoubleNumberFromInput(this);
            $input.val(NumberProvider.GetDisplayNumber($input.val()));
        });

        $('body').on('keyup', '[data-control="1"][data-input-type="normal-number"]', function (evt) {
            NumberProvider.GetIntegerNumberFromInput(this);
        });

        $('body').on('keyup', '[data-control="1"][data-input-type="number"][data-type="integer"]', function () {
            let $input = $(this);
            NumberProvider.GetIntegerNumberFromInput(this);
            $input.val(NumberProvider.GetDisplayNumber($input.val()));
        });
    }
}

$(function () {
    NumberProvider.Inits();
});