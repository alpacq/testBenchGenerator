   M S B A M L   `   `   `  ���� M  ItestBenchGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:testBenchGenerator.FIRDesigner.View#testBenchGenerator.FIRDesigner.View  1    +testBenchGenerator.FIRDesigner.View.FIRView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Clocal1clr-namespace:testBenchGenerator.FIRDesigner.View     R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  .���   � ColumnDefinitions  �� $��3* ��� $��2* � � RowDefinitions �� $	��15* ��� $��2* �.+�� $��0��$��0��$��10q� �� $��1* ��� $��1* �.+�  OxyPlot.Wpf.Plot  -     Name$ firResp��$��0��$��
10,10,10,5q�$��Stretch=�$��Stretch=�   TitleFontSize$ 14Q�
�������   Title$$ Filter Response in Time Domain��$��1q�   Series   OxyPlot.Wpf.LineSeries  ���� *FIRRespPoints+   Axes   OxyPlot.Wpf.LinearAxis    OxyPlot.Wpf.Axis   Title$
 Time��   Position$ Bottom=�	   Unit$	 s��
   LabelFormatter
 ValueAxisLabelFormatters  $
 Left=�
 ValueAxisLabelFormatter  -   $ firChart��$��1��$��
10,5,10,10q�$��Stretch=�$��Stretch=�$ 14Q�
�������$ Filter Frequency Response��$��1q�     OxyPlot.Wpf.Series   Color$
 Navy������ *FIRChartPoints+   $ 	Frequency��$ Bottom=�$	 Hz��
 ValueAxisLabelFormatterHz  $
 Left=�
 ValueAxisLabelFormatter� $��0��$��1��$��10q� �� $��1* ��� $��2* �.+�� $��0��  �� �� .+�  -   $ winResp��$��0��$��Stretch=�$��Stretch=�$ 12Q�$��
10,10,5,10q�$$ Window Function in Time Domain��$��1q�   $ Yellow������ *WinRespPoints+   $
 Time��$ Bottom=�$	 s��
 ValueAxisLabelFormatters  $
 Left=�
 ValueAxisLabelFormatter  -   $ winChart��$��1��$��Stretch=�$��Stretch=�$ 12Q�$��
5,10,10,10q�$( "Window Function Frequency Response��$��1q�   $ DarkRed������ *WinChartPoints+   $ 	Frequency��$ Bottom=�$	 Hz��
 ValueAxisLabelFormatterHz  $
 Left=�
 ValueAxisLabelFormatter� $��1�� �� �� �� �� �� �� �� ��   �� $��1* ��� $��1* �.+��� $��Sampling frequency [Hz]��$��0��$��0��$
��10,0q�$��Center=�$
��Left=��� $��Filter length��$��1��$��0��$
��10,0q�$��Center=�$
��Left=��� $��Shift samples��$��2��$��0��$
��10,0q�$��Center=�$
��Left=��� $��Cutoff frequency low [Hz]��$��3��$��0��$
��10,0q�$��Center=�$
��Left=��� $ ��Cutoff frequency high [Hz]��$��4��$��0��$
��10,0q�$��Center=�$
��Left=��� $��Window function��$��5��$��0��$
��10,0q�$��Center=�$
��Left=��� $��Filter type��$��6��$��0��$
��10,0q�$��Center=�$
��Left=��� $��Response plot��$��7��$��0��$
��10,0q�$��Center=�$
��Left=��� -    ��Name$ fs��$��0��$��1��$
��10,0q�$��Right=�$��Center=�$��Stretch=����� *Fs+�� -   $	 len��$��1��$��1��$
��10,0q�$��Right=�$��Center=�$��Stretch=����� *Length+�� -   $ ss��$��2��$��1��$
��10,0q�$��Right=�$��Center=�$��Stretch=����� *ShiftSamples+�� -   $ lf��$��3��$��1��$
��10,0q�$��Right=�$��Center=�$��Stretch=����� *	LowFreq+{��� *IsLFEnabled+�� -	   $ hf��$��4��$��1��$
��10,0q�$��Right=�$��Center=�$��Stretch=����� *
HighFreq+{��� *IsHFEnabled+�� -
    ��Name$ wt��$��5��$��1��$
��10,0q�$��Center=�$��Stretch=�.@��� $��Rectangular���� $��
Triangular���� $��Welch���� $
��Sine���� $
��Hann���� $��Hamming���� $��Blackman���� $��Nuttall���� $��Blackman-Nuttall���� $��Blackman-Harris���� $��Flat Top���� -   $ ft��$��6��$��1��$
��10,0q�$��Center=�$��Stretch=�.@��� $��Lowpass filter (LPF)���� $��Highpass filter (HPF)���� $��Bandpass filter (BPF)���� $��Bandstop filter (BSF)���� -   $ pt��$��7��$��1��$
��10,0q�$��Center=�$��Stretch=�.@��� $��Impulse���� $
��Step��� $��1��$��2��  �� $	��100 ���  �� �� .+��� $��0��$
��10,5q�$��Coefficients��$
��Bold"�$��Center=��� -   $ coeffs�� �� 
IsReadOnly . 
�������$��1��$��0,0,10,5q�$��Center=�$��Stretch=����� *Coeffs+ �� Mode$ OneWay=��� -    ��Name$ 	infoBlock��$��1��$��0��$��	100,0,0,0q�$��Center=�$
��Left=���  �� Orientation$ 
Horizontal=�$��1��$��1��$��Center=�$��Right=�$
��10,5q�.+���  � ToolTip �� *ProblemToolTip+ ^� 	IsEnabled �� *
CanUpdateN+.+��� -    ��Name$ design��$��Design��$
��Bold"�$��80��$	��5,0q�{��� *	CanDesign+��  �� *ProblemExportToolTip+ �� *
CanExportN+.+��� -   $ export��$��Export��$
��Bold"�$��80��$	��5,0q�{��� *	CanExport+