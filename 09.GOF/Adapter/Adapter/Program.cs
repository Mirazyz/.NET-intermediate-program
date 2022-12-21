using Adapter;

IContainer<int> container = new CustomElements();
Printer printer = new Printer();

printer.Print(container);