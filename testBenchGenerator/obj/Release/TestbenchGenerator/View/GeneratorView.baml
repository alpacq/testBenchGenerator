   M S B A M L   `   `   `  ���� M  ItestBenchGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:testBenchGenerator.TestbenchGenerator.View*testBenchGenerator.TestbenchGenerator.View  i9clr-namespace:testBenchGenerator.TestbenchGenerator.Model+testBenchGenerator.TestbenchGenerator.Model  >    8testBenchGenerator.TestbenchGenerator.View.GeneratorView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Jlocal8clr-namespace:testBenchGenerator.TestbenchGenerator.View     Kmodel9clr-namespace:testBenchGenerator.TestbenchGenerator.Model     -     >�Name$  genView��
������� � 	Resources >   8testBenchGenerator.TestbenchGenerator.Model.PolConverter   PolConverter��%   &	          .��� -   .+�� -    �Name$ popup1�� J� IsHitTestVisible .  $��RelativePoint=� � AllowsTransparency .  � PlacementTarget ��  �� ElementName$ genView��.���� $��2q�
���   �
������� J� Opacity$
 0.75Q�._���  �� Orientation$ 
Horizontal=�$��4,3,8,3q�.+��� $��/Resources/change.png��$��25��$��25���� $
��Bold"�$��Center=�$��8,0,0,0q����� $ genView��	 �� Path$	 DraggedItem.Description��� $ Vertical=�$��0��$��2��$
��20,0q�$��Stretch=�.+��� -   
 ��Name$
 dutpanel��$ 
Horizontal=�$��Stretch=�.+��� $��10,0,0,0q�$
��Left=�$��Center=�$#��Module under test source file���� $ 
Horizontal=�$��20q�.+��� -    ��Name$ dutFile��$	��300��$��25�� �� 
IsReadOnly . $��Center=��� -    ��Name$	 dut��$
��10,0q�$��25��$��35��$	��...���� -   $
 	dutParams��$ 
Horizontal=�$��Stretch=�.+�]� .+��� $	��Top=�$��1,1,0,0q�
���   �._��� $ Vertical=�.+��� -    ��Name$ clkpanel��$��Clocks��$
��Bold"�$��10,5,0,5q��� -	    ��Name$ clocks��$��Stretch=�$��0q�$
��Auto��$��Disabled=����� *Clocks+ �� View �� .P��� $
��Name�� �� CellTemplate �� .R��� $��Stretch=�$��Stretch=����� *Name+ � ToolTip �� *Name+�� $��Freq. (MHz)�� �� .R��� $��Stretch=�$��Center=�$��Stretch=�$��0q����� *	Frequency+ �� Mode$ TwoWay=� �� *	Frequency+]� .+��� $	��Top=�$��1,1,0,0q�
���   �._��� $ Vertical=�.+��� $��Resets��$
��Bold"�$��10,5,0,5q��� -
   $ resets��$��Stretch=�$��0q�$
��Auto��$��Disabled=����� *Resets+ �� .P��� $
��Name�� �� .R��� $��Stretch=�$��Stretch=����� *Name+ �� *Name+�� $��Polarization�� �� .R��� $��Center=�$��Stretch=�$��Stretch=�$��0q����� *Polarization+ �� 	Converter# [  $ TwoWay=� �� *Polarization+# [  ]� .+��� $	��Top=�$��1,1,0,0q�
���   �._��� $ Vertical=�.+��� $��
Parameters��$
��Bold"�$��10,5,0,5q��� -   $ paramss��$��Stretch=�$��0q�$
��Auto��$��Disabled=����� *
Parameters+ �� .P��� $
��Name�� �� .R��� $��Stretch=�$��Stretch=����� *Name+ �� *Name+�� $��Value�� �� .R��� $��Stretch=�$��Stretch=�$��0q����� *Value+$ TwoWay=� �� *Value+]� .+��� $	��Top=�$��1,1,0,0q�
���   �._��� $ Vertical=�.+��� $��Inputs��$
��Bold"�$��10,5,0,5q��� -   $ inputs��$��Stretch=�$��0q�$
��Auto��$��Disabled=����� *Inputs+ �� .P��� $
��Name�� �� .R��� $��Stretch=�$��Stretch=����� *Name+ �� *Name+�� $��Bitwidth�� �� .R��� $��Stretch=�$��Stretch=����� *
Bitwidth+ �� *
Bitwidth+]� .+��� $	��Top=�$��1,1,1,0q�
���   �._��� $ Vertical=�.+��� $��Outputs��$
��Bold"�$��10,5,0,5q��� -   $ outputs��$��Stretch=�$��0q�$
��Auto��$��Disabled=����� *	Outputs+����  �� 
TargetType" ��.���  �� Property � �� �� Value$ Stretch=� �� .P��� $
��Name�� �� .R��� $��Stretch=�$��Stretch=����� *Name+ �� *Name+�� $��Bitwidth�� �� .R��� $��Stretch=�$��Stretch=����� *
Bitwidth+ �� *
Bitwidth+�� $ 
Horizontal=�$��Stretch=�.+�]� .+��� $	��Top=�$��1q�
���   �._��� $ Vertical=�$��Stretch=�.+��� -   $ tcpanel��$��
Test Cases��$
��Bold"�$��10,5,0,5q��� -   $ datains��$��Stretch=�$��0q�$
��Auto������ *	TestCases+ �� SelectedItem �� *SelectedTestCase+ �� .P��� $��No�� �� .R��� $	��5,0q�$��Center=�$��Stretch=����� *Order+�� $��
Clock sync�� �� .R��� $��Stretch=�$��Stretch=����� *	ClockSync+ �� *	ClockSync+�� $��Input ports�� �� .R��� $��Stretch=�$��Stretch=����� *DIs+ �� *DIs+�� $��Input valid port�� �� .R��� $��Stretch=�$��Stretch=����� *	ValidIn+ �� *	ValidIn+�� $��Input pattern�� �� .R��� $��Stretch=�$��Stretch=����� *
DataVector+ �� *
DataVector+�� $��Valid sequence�� �� .R��� $��Stretch=�$��Stretch=����� *VldSeq+ �� *VldSeq+�� $��Output ports�� �� .R��� $��Stretch=�$��Stretch=����� *DOs+ �� *DOs+�� $��Output valid port�� �� .R��� $��Stretch=�$��Stretch=����� *
ValidOut+ �� *
ValidOut+�� $��Radix�� �� .R��� $��Stretch=�$��Stretch=����� *Radix+ �� *Radix+�� $
��Loop�� �� .R��� $	��5,0q�$��Center=� d� 	IsChecked �� *Loop+�� -   $
 	btnspanel��$ 
Horizontal=�$��6��$��2��$��Center=�$��Right=�$��
0,10,10,10q�.+���  �� *ProblemAddToolTip+ ^� 	IsEnabled �� *	CanAddN+.+��� -   $	 add��$	��Add��$��Center=�$��Center=�$��50��$��0,0,10,0q�{��� *CanAdd+��  �� *ProblemRemoveToolTip+ �� *
CanRemoveN+.+��� -   $ remove��$��Remove��$��Center=�$��Center=�$��50��{��� *	CanRemove+�� -   $
 genpanel��$ 
Horizontal=�$��Stretch=�$��Right=�$��	0,10,0,10q�.+��� -   $ 	infoBlock��$��Center=�$��Center=�$��3��$��0����  �� *ProblemGenerateToolTip+ �� *CanGenerateN+.+��� -   $	 run��$��3��$��1��$��Right=�$��Center=�$��25��$��70��$��Generate��{��� *CanGenerate+