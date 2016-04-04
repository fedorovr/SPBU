import pandas as pd

cellid_str = 'Cell ID'
address_str = 'Address'

report_df = pd.read_csv('../report.csv')
report_df_lacs = report_df.drop_duplicates(subset=[cellid_str])
report_df_lacs = report_df_lacs.dropna(subset=[cellid_str])
report_df_lacs.drop(address_str, axis=1, inplace=True)

report_df_lacs.to_csv('report_cleared.csv', index=False)
