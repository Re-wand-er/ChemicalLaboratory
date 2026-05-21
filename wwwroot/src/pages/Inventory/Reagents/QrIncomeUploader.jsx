import React, { useState } from 'react';
import { Box, Button, Typography, List, ListItem, ListItemText, IconButton, CircularProgress } from '@mui/material';
import PhotoCameraIcon from '@mui/icons-material/PhotoCamera';
import DeleteIcon from '@mui/icons-material/Delete';
import FilePresentIcon from '@mui/icons-material/FilePresent';

const QrIncomeUploader = ({ onFilesSelected, loading }) => {
  const [selectedFiles, setSelectedFiles] = useState([]);

  const handleFileChange = (event) => {
    const files = Array.from(event.target.files);
    if (files.length === 0) return;

    const updatedFiles = [...selectedFiles, ...files];
    
    setSelectedFiles(updatedFiles);
    onFilesSelected(updatedFiles); 
  };

  const handleRemoveFile = (indexToRemove) => {
    const updatedFiles = selectedFiles.filter((_, index) => index !== indexToRemove);
    setSelectedFiles(updatedFiles);
    onFilesSelected(updatedFiles);
  };

  return (
    <Box sx={{ width: '100%', mt: 1 }}>
      {/* Скрытый инпут */}
      <input
        accept="image/*"
        style={{ display: 'none' }}
        id="qr-modal-upload-input"
        multiple
        type="file"
        onChange={handleFileChange}
        disabled={loading}
      />

      {/* Зона клика / Сброса */}
      <label htmlFor="qr-modal-upload-input">
        <Box
          sx={{
            border: '2px dashed',
            borderColor: loading ? 'action.disabled' : 'primary.main',
            borderRadius: 2,
            p: 3,
            textAlign: 'center',
            backgroundColor: 'action.hover',
            cursor: loading ? 'not-allowed' : 'pointer',
            transition: '0.2s',
            '&:hover': {
              backgroundColor: loading ? 'action.hover' : 'action.selected'
            }
          }}
        >
          <PhotoCameraIcon sx={{ fontSize: 40, color: loading ? 'text.disabled' : 'primary.main', mb: 1 }} />
          <Typography variant="body1" fontWeight={500}>
            {loading ? 'Идет сканирование...' : 'Выбрать или перетащить фотографии QR'}
          </Typography>
          <Typography variant="caption" color="text.secondary">
            Поддерживаются файлы изображений реактивов (пакетная загрузка)
          </Typography>
        </Box>
      </label>

      {/* Список выбранных файлов для контроля */}
      {selectedFiles.length > 0 && (
        <Box sx={{ mt: 2, maxHeight: 200, overflowY: 'auto', border: '1px solid', borderColor: 'divider', borderRadius: 1 }}>
          <List dense>
            {selectedFiles.map((file, index) => (
              <ListItem
                key={index}
                secondaryAction={
                  <IconButton edge="end" aria-label="delete" onClick={() => handleRemoveFile(index)} disabled={loading}>
                    <DeleteIcon fontSize="small" color="error" />
                  </IconButton>
                }
              >
                <FilePresentIcon sx={{ color: 'text.secondary', mr: 1 }} />
                <ListItemText 
                  primary={file.name} 
                  secondary={`${(file.size / 1024).toFixed(1)} КБ`}
                  primaryTypographyProps={{ noWrap: true, variant: 'body2', fontWeight: 500 }}
                />
              </ListItem>
            ))}
          </List>
        </Box>
      )}
    </Box>
  );
};

export default QrIncomeUploader;
