   M S B A M L   `   `   `  ����M  ItestBenchGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:testBenchGenerator.FIRDesigner.View#testBenchGenerator.FIRDesigner.View  5      1    +testBenchGenerator.FIRDesigner.View.FIRView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Clocal1clr-namespace:testBenchGenerator.FIRDesigner.View     5      R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $��3* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $	��15* �6   �� 5      $��2* �6   .+�5   
   � $��0��6   $��0��6   $��10q�6,    5      �� 5      $��1* �6    �� 5      $��1* �6    .+�5        OxyPlot.Wpf.Plot  -   5          Name$ firResp��5      $��0��6(   $��
10,10,10,5q�65   $��Stretch=�6I   $��Stretch=�6e      TitleFontSize$ 14Q�6�   
�������   Title$$ Filter Response in Time Domain��6�   $��1q�6�      Series 5        OxyPlot.Wpf.LineSeries  5      ��6%   �� *FIRRespPoints+   Axes 5        OxyPlot.Wpf.LinearAxis  5        OxyPlot.Wpf.Axis   Title$
 Time��6%      Position$ Bottom=�62   	   Unit$	 s��6D   
   LabelFormatter
 ValueAxisLabelFormatters6M     5      $
 Left=�6%   
 ValueAxisLabelFormatter65   5         5!      -   5        $ firChart��5!      $��1��6)   $��
10,5,10,10q�66   $��Stretch=�6J   $��Stretch=�6f   $ 14Q�6�   
�������$ Filter Frequency Response��6�   $��1q�6�    5"        5#        OxyPlot.Wpf.Series   Color$
 Navy��6L   ��6%   �� *FIRChartPoints+6L    5%        5&      $ 	Frequency��6%   $ Bottom=�67   $	 Hz��6I   
 ValueAxisLabelFormatterHz6S     5'      $
 Left=�6%   
 ValueAxisLabelFormatter65   5)      5*      � 5,   
   $��0��6   $��1��6   $��10q�6,    5-      �� 5.      $��1* �6    �� 5/      $��2* �6    .+�52      � $��0��6     53      �� 54      �� 55      .+�57        -   5        $ winResp��57      $��0��6,   $��Stretch=�6<   $��Stretch=�6X   $ 12Q�6v   $��
10,10,5,10q�6�   $$ Window Function in Time Domain��6�   $��1q�6�    58        59      $ Yellow��6O   ��6)   �� *WinRespPoints+6O    5;        5<      $
 Time��6)   $ Bottom=�66   $	 s��6H   
 ValueAxisLabelFormatters6Q     5=      $
 Left=�6)   
 ValueAxisLabelFormatter69   5?        5@      -   5        $ winChart��5@      $��1��6-   $��Stretch=�6=   $��Stretch=�6Y   $ 12Q�6w   $��
5,10,10,10q�6�   $( "Window Function Frequency Response��6�   $��1q�6�    5A        5B      $ DarkRed��6P   ��6)   �� *WinChartPoints+6P    5D        5E      $ 	Frequency��6)   $ Bottom=�6;   $	 Hz��6M   
 ValueAxisLabelFormatterHz6W     5F      $
 Left=�6)   
 ValueAxisLabelFormatter69   5H      5I      � 5K      $��1��6    5L      �� 5M      �� 5N      �� 5O      �� 5P      �� 5Q      �� 5R      �� 5S      �� 5T        5V      �� 5W      $��1* �6'   �� 5X      $��1* �6'   .+�5[      �� $��Sampling frequency [Hz]��6   $��0��6;   $��0��6H   $
��10,0q�6X   $��Center=�6f   $
��Left=�6�   �� 5\      $��Filter length��6   $��1��61   $��0��6>   $
��10,0q�6N   $��Center=�6\   $
��Left=�6w   �� 5]      $��Shift samples��6   $��2��61   $��0��6>   $
��10,0q�6N   $��Center=�6\   $
��Left=�6w   �� 5^      $��Cutoff frequency low [Hz]��6   $��3��6=   $��0��6J   $
��10,0q�6Z   $��Center=�6h   $
��Left=�6�   �� 5_      $ ��Cutoff frequency high [Hz]��6   $��4��6>   $��0��6K   $
��10,0q�6[   $��Center=�6i   $
��Left=�6�   �� 5`      $��Window function��6   $��5��63   $��0��6@   $
��10,0q�6P   $��Center=�6^   $
��Left=�6y   �� 5a      $��Filter type��6   $��6��6/   $��0��6<   $
��10,0q�6L   $��Center=�6Z   $
��Left=�6u   �� 5b      $��Response plot��6   $��7��61   $��0��6>   $
��10,0q�6N   $��Center=�6\   $
��Left=�6w   �� 5d      -   5         ��Name$ fs��5d      $��0��6&   $��1��63   $
��10,0q�6C   $��Right=�6e   $��Center=�6�   $��Stretch=�6�   ��6Q   �� *Fs+6�   �� 5e      -   5        $	 len��5e      $��1��6'   $��1��64   $
��10,0q�6D   $��Right=�6j   $��Center=�6�   $��Stretch=�6�   ��6R   �� *Length+6�   �� 5f      -   5        $ ss��5f      $��2��6&   $��1��63   $
��10,0q�6C   $��Right=�6o   $��Center=�6�   $��Stretch=�6�   ��6Q   �� *ShiftSamples+6�   �� 5g      -   5        $ lf��5g      $��3��6&   $��1��63   $
��10,0q�6C   $��Right=�6j   $��Center=�6�   $��Stretch=�6�   ��6Q   �� *	LowFreq+{�6�   �� *IsLFEnabled+6�   �� 5h      -	   5        $ hf��5h      $��4��6&   $��1��63   $
��10,0q�6C   $��Right=�6k   $��Center=�6�   $��Stretch=�6�   ��6Q   �� *
HighFreq+{�6�   �� *IsHFEnabled+6   �� 5i      -
   5         ��Name$ wt��5i      $��5��6'   $��1��64   $
��10,0q�6D   $��Center=�6R   $��Stretch=�6m   .@�5j      �� $��Rectangular��6#   �� 5k      $��
Triangular��6#   �� 5l      $��Welch��6#   �� 5m      $
��Sine��6#   �� 5n      $
��Hann��6#   �� 5o      $��Hamming��6#   �� 5p      $��Blackman��6#   �� 5q      $��Nuttall��6#   �� 5r      $��Blackman-Nuttall��6#   �� 5s      $��Blackman-Harris��6#   �� 5t      $��Flat Top��6#   5u      �� 5v      -   5        $ ft��5v      $��6��6'   $��1��64   $
��10,0q�6D   $��Center=�6R   $��Stretch=�6m   .@�5w      �� $��Lowpass filter (LPF)��6#   �� 5x      $��Highpass filter (HPF)��6#   �� 5y      $��Bandpass filter (BPF)��6#   �� 5z      $��Bandstop filter (BSF)��6#   5{      �� 5|      -   5        $ pt��5|      $��7��6'   $��1��64   $
��10,0q�6D   $��Center=�6R   $��Stretch=�6m   .@�5}      �� $��Impulse��6#   �� 5~      $
��Step��6#   5      5�      5�      � 5�   
   $��1��6   $��2��6     5�      �� 5�      $	��100 �6#   �� 5�       5�      �� 5�      �� 5�      .+�5�      �� $��0��6   $
��10,5q�6(   $��Coefficients��66   $
��Bold"�6J   $��Center=�6\   �� 5�      -   5        $ coeffs��5�       �� 
IsReadOnly . 
�������$��1��6O   $��0,0,10,5q�6�   $��Center=�6�   $��Stretch=�6�   ��6_   �� *Coeffs+ �� Mode$ OneWay=�6�   �� 5�      -   5         ��Name$ 	infoBlock��5�      $��1��6+   $��0��68   $��	100,0,0,0q�6H   $��Center=�6[   $
��Left=�6v   �� 5�       �� Orientation$ 
Horizontal=�6   $��1��62   $��1��6?   $��Center=�6O   $��Right=�6j   $
��10,5q�6�   .+�5�      ��  � ToolTip 6   �� *ProblemToolTip+ ^� 	IsEnabled 6@   �� *
CanUpdateN+.+�5�      �� -   5         ��Name$ design��5�      $��Design��6-   $
��Bold"�6S   $��80��6e   $	��5,0q�6p   {�6}   �� *	CanDesign+5�      �� 5�       6   �� *ProblemExportToolTip+ 6F   �� *
CanExportN+.+�5�      �� -   5        $ export��5�      $��Export��6-   $
��Bold"�6S   $��80��6e   $	��5,0q�6p   {�6}   �� *	CanExport+5�      5�      5�      5�      5�      