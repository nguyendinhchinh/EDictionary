## Data structures
* Dictionary:
	* Đơn giản
	* Gần như không cần thiết lập vì đã có thư viện built-in sẵn
	* Tốc độ search rất nhanh, không phụ thuộc vào kích cỡ từ điển (O(1))
	* Không thể search prefix hiệu quả vì là unordered list
* Order List + Binary Search:
	* Đơn giản
	* Tốc độ search nhanh trong hầu hết trường hợp dù phụ thuộc vào kích cỡ từ điển (O(log n))
	* Có thể search prefix
* Prefix Tree:
	* Phức tạp hơn hai cách trên
	* Tốc độ search rât nhanh, vì chỉ phụ thuộc vào số ký tự trong 1 từ
	* Có thể search prefix

## Features (Sorted by priority)
	* Search word
	* Autocomplete
	* Spellcheck word when not found or on more results
	* Open dictionary in taskbar to pop up mini window to search
	* Highlight word and right click to search
	* Default is to use extended-search mode. In this mode, multiple patterns is delimited by spaces
		* Other mode: regex, exact-match, glob

## Roadmap
* Spellcheck
	* Github link

## Credit
Data structures:
https://futurice.com/blog/data-structures-for-fast-autocomplete
http://igoro.com/archive/efficient-auto-complete-with-a-ternary-search-tree/
https://en.wikipedia.org/wiki/Trie
[Why use binary search](https://stackoverflow.com/questions/8212070/ternary-search-tree-with-all-suggestions-autocomplete-in-c-sharp)
