   M S B A M L   `   `   `  ���� M  ItestBenchGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:testBenchGenerator.TestbenchGenerator.View*testBenchGenerator.TestbenchGenerator.View  :    4testBenchGenerator.TestbenchGenerator.View.NewTCView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Jlocal8clr-namespace:testBenchGenerator.TestbenchGenerator.View       � Title$  New Test Case��$	��450��$
��1330��.��� $��0,0,10,0q� � ColumnDefinitions �� $��1* ��� $��1* ��� $��1* � � RowDefinitions �� �� $��5* ��� .+�� $��0��$��2��$��0,15,5,0q� �� �� �� �� �� �� �� �� �� .+��� $��0��$��0��$��
Clock sync��$��Center=�$��Center=��� $��2��$��0��$��Input pattern��$��Center=�$��Center=��� $��4��$��0��$��Radix��$��Center=�$��Center=��� $��6��$��0��$��Valid sequence��$��Center=�$��Center=���  �� Orientation$ 
Horizontal=�$��8��$��0��$��Stretch=�$��Center=�.+��� $
��Loop��$��50,10q�$��Center=�$
��Left=��� -    ��Name$
 loop��$��50,10q�$��Center=�$��Center=� d� 	IsChecked �� *Loop+�� -   $��clkCB��$��1��$
��50,5q�$��Stretch=�$��Center=����� *Clocks+ �� SelectedItem �� *Clock+�� $��3��$ Vertical=�$��Stretch=�$��Center=�$��2q�.+��� $ 
Horizontal=�.+��� -    ��Name$ inFile��$	��270��$��25�� �� 
IsReadOnly . $��Center=�$��2,2,0,0q����� *	InputPath+�� -   	 ��Name$	 in��$��	10,2,10,0q�$��25��$��35��$	��...���� -   $	��rCB��$��5��$
��50,5q�$��Stretch=�$��Center=����� *	Radixes+ �� *Radix+�� -   $
��vsCB��$��7��$
��50,5q�$��Stretch=�$��Center=����� *Seqs+ �� *Seq+� $��1��$��2��$��5,15,5,0q� �� �� $��6* ��� �� .+��� $��0��$��Input ports��$��Center=�$��Center=��� $��2��$��Input valid port��$��Center=�$��Center=��� -   $
��diLB��$��1��$��Stretch=�$��2q�
 �� SelectionMode$
 Multiple=����� *Ins+���� .R�� $	��0,2q� �� $��* ��� $��50 �.+��� $��0������ *Name+�� -   $��1�� �� *IsSel+�� -	   $��vipCB��$��3��$
��50,5q�$��Stretch=�$��Center=����� *Inputs+ �� *	ValidIn+� $��2��$��2��$��5,15,0,0q� �� �� $��6* ��� �� .+��� $��0��$��Output ports��$��Center=�$��Center=��� $��2��$��Output valid port��$��Center=�$��Center=��� -
   $
��doLB��$��1��$��Stretch=�$��2q�$
 Multiple=����� *Outs+���� .R�� $	��0,2q� �� $��* ��� $��50 �.+��� $��0������ *Name+�� -   $��1�� �� *IsSel+�� -   $��vopCB��$��3��$
��50,5q�$��Stretch=�$��Center=����� *	Outputs+ �� *
ValidOut+�� $ 
Horizontal=�$��6��$��2��$��Center=�$��Right=�$��0,0,30,0q�.+���  � ToolTip �� *ProblemToolTip+ ^� 	IsEnabled �� *
CanSaveN+.+��� -   $		 add��$��Center=�$��Center=�$��50��$��0,0,10,0q����� *	AddText+{��� *	CanSave+�� -   $	 cancel��$��Cancel��$��Center=�$��Center=�$��50��